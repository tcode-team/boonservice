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
    public class SapContext: DbContext
    {
        public SapContext() : base("SAP") {
            Database.SetInitializer<LoadContext>(null);
        }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SAPSR3");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<ShipmentSearchModel>();

            modelBuilder.Entity<VTTK>()
                .ToTable("VTTK")
                .HasKey(t => new { t.MANDT, t.TKNUM });

            modelBuilder.Entity<T173T>()
                .ToTable("T173T")
                .HasKey(t => new { t.MANDT, t.SPRAS, t.VSART });
        }

        public DbSet<VTTK> VTTK { get; set; }
        public DbSet<T173T> T173T { get; set; }
    }
}