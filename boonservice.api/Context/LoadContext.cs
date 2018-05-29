using boonservice.api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace boonservice.api.Context
{
    public class LoadContext: DbContext
    {
        public LoadContext() : base("LOAD") {
            Database.SetInitializer<LoadContext>(null);
        }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(string.Empty);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Ignore<B3G_USERS>();
            modelBuilder.Entity<B3G_USERS>()
                .ToTable("B3G_USERS")
                .Property(t => t.USER_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            //modelBuilder.Entity<B3G_USERS>().HasMany(t => t.B3G_USER_DESCs);

            modelBuilder.Entity<B3G_USER_DESC>()
                .ToTable("B3G_USER_DESC")
                .Property(t => t.USER_DESC_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }

        public DbSet<B3G_USERS> B3G_USERS { get; set; }
        public DbSet<B3G_USER_DESC> B3G_USER_DESC { get; set; }
    }
}