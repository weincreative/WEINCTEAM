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
    
    public partial class hst_randevu
    {
        public int t_id { get; set; }
        public Nullable<int> t_basvuru { get; set; }
        public string t_tc { get; set; }
        public string t_title { get; set; }
        public string t_aciklama { get; set; }
        public string t_baslangicsaat { get; set; }
        public string t_bitissaat { get; set; }
        public string t_classname { get; set; }
        public string t_icon { get; set; }
        public bool t_allday { get; set; }
        public string t_createuser { get; set; }
        public System.DateTime t_createdate { get; set; }
        public int t_basvurudr { get; set; }
        public bool t_aktif { get; set; }
    
        public virtual hst_hastakarti hst_hastakarti { get; set; }
    }
}
