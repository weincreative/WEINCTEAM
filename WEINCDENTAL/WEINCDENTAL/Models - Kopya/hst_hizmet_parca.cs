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
    
    public partial class hst_hizmet_parca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hst_hizmet_parca()
        {
            this.hst_hizmet = new HashSet<hst_hizmet>();
        }
    
        public int t_id { get; set; }
        public string t_adi { get; set; }
        public string t_createuser { get; set; }
        public System.DateTime t_createdate { get; set; }
        public bool t_aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_hizmet> hst_hizmet { get; set; }
    }
}