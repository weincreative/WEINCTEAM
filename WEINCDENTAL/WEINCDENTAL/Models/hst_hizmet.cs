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
    
    public partial class hst_hizmet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hst_hizmet()
        {
            this.hst_his_hareket = new HashSet<hst_his_hareket>();
        }
    
        public int t_id { get; set; }
        public string t_adi { get; set; }
        public int t_parcauygunmu { get; set; }
        public int t_çeneuygunmu { get; set; }
        public string t_fiyat { get; set; }
        public string t_createuser { get; set; }
        public System.DateTime t_createdate { get; set; }
        public bool t_aktif { get; set; }
    
        public virtual hst_cene_uygunmu hst_cene_uygunmu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_his_hareket> hst_his_hareket { get; set; }
        public virtual hst_hizmet_parca hst_hizmet_parca { get; set; }
    }
}
