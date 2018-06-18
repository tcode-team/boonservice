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
    /// DBContext to SAPSR3 owner
    /// </summary>
    public class SAPSR3Context: DbContext
    {
        public SAPSR3Context() : base("SAP") {
            Database.SetInitializer<SAPSR3Context>(null);
        }

        public DbSet<VTTK> VTTK { get; set; }
        public DbSet<T173T> T173T { get; set; }
        public DbSet<TVROT> TVROT { get; set; }
        public DbSet<VBAK> VBAK { get; set; }
        public DbSet<VBAP> VBAP { get; set; }
        public DbSet<VBPA> VBPA { get; set; }
        public DbSet<ADRC> ADRC { get; set; }
        public DbSet<KNA1> KNA1 { get; set; }
        public DbSet<VBRK> VBRK { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SAPSR3");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<ShipmentSearchModel>();

            modelBuilder.Entity<VTTK>();
            modelBuilder.Entity<T173T>();
            modelBuilder.Entity<TVROT>();
            modelBuilder.Entity<VBAK>()
                .HasKey(k => new { k.MANDT, k.VBELN});
            modelBuilder.Entity<VBAP>()
                .HasKey(k => new { k.MANDT, k.VBELN, k.POSNR });
            modelBuilder.Entity<VBPA>()
                .HasKey(k => new { k.MANDT, k.VBELN, k.PARVW });
            modelBuilder.Entity<KNA1>()
                .HasKey(k => new { k.MANDT, k.KUNNR });
            modelBuilder.Entity<ADRC>()
                .HasKey(k => new { k.CLIENT, k.ADDRNUMBER, k.DATE_FROM, k.NATION });
            modelBuilder.Entity<VBRK>()
               .HasKey(k => new { k.MANDT, k.VBELN });
        }
    }
}