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
    
    public partial class hst_il
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hst_il()
        {
            this.hst_hastakarti = new HashSet<hst_hastakarti>();
            this.hst_ilce = new HashSet<hst_ilce>();
        }
    
        public int t_id { get; set; }
        public int t_ulkeId { get; set; }
        public string t_adi { get; set; }
        public bool t_aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_hastakarti> hst_hastakarti { get; set; }
        public virtual hst_ulke hst_ulke { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_ilce> hst_ilce { get; set; }
    }
}