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
    
    public partial class adm_pacs
    {
        public adm_pacs()
        {
            this.hst_basvuru = new HashSet<hst_basvuru>();
        }
    
        public int t_id { get; set; }
        public string t_pacspath { get; set; }
        public Nullable<decimal> t_tc { get; set; }
        public string t_createuser { get; set; }
        public Nullable<System.DateTime> t_tarih { get; set; }
        public Nullable<int> t_aktif { get; set; }
    
        public virtual hst_hastakarti hst_hastakarti { get; set; }
        public virtual ICollection<hst_basvuru> hst_basvuru { get; set; }
    }
}
