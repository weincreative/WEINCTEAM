//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEINCDENTAL_CALLINGSCREEN
{
    using System;
    using System.Collections.Generic;
    
    public partial class hst_ulke
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hst_ulke()
        {
            this.hst_hastakarti = new HashSet<hst_hastakarti>();
            this.hst_il = new HashSet<hst_il>();
        }
    
        public int CountryID { get; set; }
        public string BinaryCode { get; set; }
        public string TripleCode { get; set; }
        public string CountryName { get; set; }
        public string PhoneCode { get; set; }
        public bool Aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_hastakarti> hst_hastakarti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_il> hst_il { get; set; }
    }
}
