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
    
    public partial class hst_medenidurum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hst_medenidurum()
        {
            this.hst_hastakarti = new HashSet<hst_hastakarti>();
        }
    
        public int t_id { get; set; }
        public string t_adi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_hastakarti> hst_hastakarti { get; set; }
    }
}
