﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEINCDENTAL.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WEINCDENTALEntities : DbContext
    {
        public WEINCDENTALEntities()
            : base("name=WEINCDENTALEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<adm_kullanicigrup> adm_kullanicigrup { get; set; }
        public virtual DbSet<adm_kullanicilar> adm_kullanicilar { get; set; }
        public virtual DbSet<adm_modulyetki> adm_modulyetki { get; set; }
        public virtual DbSet<adm_pacs> adm_pacs { get; set; }
        public virtual DbSet<hst_basvuru> hst_basvuru { get; set; }
        public virtual DbSet<hst_bölüm> hst_bölüm { get; set; }
        public virtual DbSet<hst_cene_uygunmu> hst_cene_uygunmu { get; set; }
        public virtual DbSet<hst_cinsiyet> hst_cinsiyet { get; set; }
        public virtual DbSet<hst_hastakarti> hst_hastakarti { get; set; }
        public virtual DbSet<hst_his_hareket> hst_his_hareket { get; set; }
        public virtual DbSet<hst_hizmet> hst_hizmet { get; set; }
        public virtual DbSet<hst_hizmet_parca> hst_hizmet_parca { get; set; }
        public virtual DbSet<hst_il> hst_il { get; set; }
        public virtual DbSet<hst_ilçe> hst_ilçe { get; set; }
        public virtual DbSet<hst_medenidurum> hst_medenidurum { get; set; }
        public virtual DbSet<hst_randevu> hst_randevu { get; set; }
        public virtual DbSet<hst_vezne> hst_vezne { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
