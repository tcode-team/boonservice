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
            Database.SetInitializer<LoadContext>(null);
        }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(String.Empty);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<afs_car_group>()
                .ToTable("afs_car_group")
                .HasKey(t => new { t.cargroup_code });

            modelBuilder.Entity<afs_car_identity_card>()
                .ToTable("afs_car_identity_card")
                .HasKey(t => new { t.people_id });

            modelBuilder.Entity<afs_carries_point>()
                .ToTable("afs_carries_point")
                .HasKey(t => new { t.point_id });

            modelBuilder.Entity<afs_expense>()
                .ToTable("afs_expense")
                .HasKey(t => new { t.expense_id });

            modelBuilder.Entity<afs_shipment_carries>()
                .ToTable("afs_shipment_carries")
                .HasKey(t => new { t.client, t.shipment_number, t.item_no });

            modelBuilder.Entity<afs_shipment_expense>()
                .ToTable("afs_shipment_expense")
                .HasKey(t => new { t.client, t.shipment_number, t.expense_id });

            modelBuilder.Entity<afs_shipment_h>()
                .ToTable("afs_shipment_h")
                .HasKey(t => new { t.client, t.shipment_number });
        }

        public DbSet<afs_car_group> afs_car_group { get; set; }
        public DbSet<afs_car_identity_card> afs_car_identity_card { get; set; }
        public DbSet<afs_carries_point> afs_carries_point { get; set;  }
        public DbSet<afs_expense> afs_expense { get; set; }
        public DbSet<afs_shipment_carries> afs_shipment_carries { get; set; }
        public DbSet<afs_shipment_expense> afs_shipment_expense { get; set; }
        public DbSet<afs_shipment_h> afs_shipment_h { get; set; }
    }
}