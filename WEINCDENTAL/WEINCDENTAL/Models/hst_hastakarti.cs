//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEINCDENTAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class hst_hastakarti
    {
        public hst_hastakarti()
        {
            this.adm_pacs = new HashSet<adm_pacs>();
            this.hst_his_hareket = new HashSet<hst_his_hareket>();
        }
    
        public int t_id { get; set; }
        public decimal t_tc { get; set; }
        public string t_adi { get; set; }
        public string t_soyadi { get; set; }
        public Nullable<int> t_cinsiyet { get; set; }
        public Nullable<int> t_medenidurum { get; set; }
        public Nullable<System.DateTime> t_dogumtarihi { get; set; }
        public Nullable<int> t_dogumyeri { get; set; }
        public Nullable<decimal> t_tel1 { get; set; }
        public Nullable<decimal> t_tel2 { get; set; }
        public Nullable<int> t_il { get; set; }
        public Nullable<int> t_ilce { get; set; }
        public string t_adres { get; set; }
        public Nullable<bool> t_aktif { get; set; }
    
        public virtual ICollection<adm_pacs> adm_pacs { get; set; }
        public virtual hst_cinsiyet hst_cinsiyet { get; set; }
        public virtual hst_il hst_il { get; set; }
        public virtual hst_il hst_il1 { get; set; }
        public virtual hst_medenidurum hst_medenidurum { get; set; }
        public virtual t_ilçe t_ilçe { get; set; }
        public virtual ICollection<hst_his_hareket> hst_his_hareket { get; set; }
    }
}
