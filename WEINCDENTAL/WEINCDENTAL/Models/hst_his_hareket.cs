//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class hst_his_hareket
    {
        public int t_id { get; set; }
        public int t_basvuruid { get; set; }
        public decimal t_tc { get; set; }
        public int t_hizmetkodu { get; set; }
        public System.DateTime t_islemtarihi { get; set; }
        public int t_diskodu { get; set; }
        public int t_parca { get; set; }
        public int t_cene { get; set; }
        public int t_borc { get; set; }
        public int t_makbuz { get; set; }
        public string t_createuser { get; set; }
        public System.DateTime t_createdate { get; set; }
        public bool t_aktif { get; set; }
    
        public virtual hst_basvuru hst_basvuru { get; set; }
        public virtual hst_hastakarti hst_hastakarti { get; set; }
        public virtual hst_hizmet hst_hizmet { get; set; }
        public virtual hst_vezne hst_vezne { get; set; }
    }
}
