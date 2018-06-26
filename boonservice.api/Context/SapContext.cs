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
    /// <summary>
    /// DBContext to B3G owner
    /// </summary>
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
        public DbSet<afs_repair_header> afs_repair_header { get; set; }
        public DbSet<afs_repair_items> afs_repair_items { get; set; }
        public DbSet<afs_repair_images> afs_repair_images { get; set; }
        public DbSet<afs_repair_appointment> afs_repair_appointment { get; set; }
        public DbSet<afs_repair_raw> afs_repair_raw { get; set; }
        public DbSet<afs_car_license> afs_car_license { get; set; }
        public DbSet<afs_repair_item_type> afs_repair_item_type { get; set; }
        public DbSet<afs_authority> afs_authority { get; set; }

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
                .HasKey(k => new { k.CLIENT, k.SHIPMENT_NUMBER, k.ITEM_NO });

            modelBuilder.Entity<afs_shipment_h>()
                .HasKey(k => new { k.CLIENT, k.SHIPMENT_NUMBER });

            modelBuilder.Entity<afs_shipment_status>();

            modelBuilder.Entity<afs_repair_header>()
                .HasKey(k => k.REPAIR_CODE);

            modelBuilder.Entity<afs_repair_items>()
                .HasKey(k => k.REPAIR_ITEM_ID);

            modelBuilder.Entity<afs_repair_images>()
                .HasKey(k => k.REPAIR_IMAGE_ID)
                .HasIndex(i => i.REPAIR_ITEM_ID);

            modelBuilder.Entity<afs_repair_appointment>()
                .HasKey(k => k.REPAIR_APPOINTMENT_ID)
                .HasIndex(i => i.REPAIR_CODE);

            modelBuilder.Entity<afs_repair_raw>()
                .HasKey(k => k.REPAIR_RAW_ID)
                .HasIndex(i => i.REPAIR_CODE);

            modelBuilder.Entity<afs_car_license>()
                .HasKey(k => k.CAR_ID);

            modelBuilder.Entity<afs_repair_item_type>()
                .HasKey(k => k.ITEM_TYPE);

            modelBuilder.Entity<afs_authority>()
                .HasKey(k => k.USER_ID);
        }

    }
}