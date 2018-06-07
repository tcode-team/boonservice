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
    public class SAPSR3Context: DbContext
    {
        public SAPSR3Context() : base("SAP") {
            Database.SetInitializer<SAPSR3Context>(null);
        }

        public DbSet<VTTK> VTTK { get; set; }
        public DbSet<T173T> T173T { get; set; }
        public DbSet<TVROT> TVROT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SAPSR3");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<ShipmentSearchModel>();

            modelBuilder.Entity<VTTK>();

            modelBuilder.Entity<T173T>();

            modelBuilder.Entity<TVROT>();
        }
    }
}