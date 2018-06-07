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
    public class SAPContext: DbContext
    {
        public SAPContext() : base("SAP") {
            Database.SetInitializer<SAPContext>(null);
        }

        public DbSet<afs_car_group> afs_car_group { get; set; }
        public DbSet<afs_car_identity_card> afs_car_identity_card { get; set; }
        public DbSet<afs_carries_point> afs_carries_point { get; set; }
        public DbSet<afs_expense> afs_expense { get; set; }
        public DbSet<afs_shipment_carries> afs_shipment_carries { get; set; }
        public DbSet<afs_shipment_expense> afs_shipment_expense { get; set; }
        public DbSet<afs_shipment_h> afs_shipment_h { get; set; }
        public DbSet<afs_shipment_status> afs_shipment_status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(String.Empty);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<afs_car_group>();

            modelBuilder.Entity<afs_car_identity_card>();

            modelBuilder.Entity<afs_carries_point>();

            modelBuilder.Entity<afs_expense>();

            modelBuilder.Entity<afs_shipment_carries>()
                .HasKey(k => new { k.CLIENT, k.SHIPMENT_NUMBER, k.ITEM_NO });

            modelBuilder.Entity<afs_shipment_expense>()
                .HasKey(k => new { k.CLIENT, k.SHIPMENT_NUMBER, k.EXPENSE_ID });

            modelBuilder.Entity<afs_shipment_h>()
                .HasKey(k => new { k.CLIENT, k.SHIPMENT_NUMBER });

            modelBuilder.Entity<afs_shipment_status>();
        }

    }
}