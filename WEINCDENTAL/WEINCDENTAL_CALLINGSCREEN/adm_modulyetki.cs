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
    
    public partial class adm_modulyetki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public adm_modulyetki()
        {
            this.adm_kullanicilar = new HashSet<adm_kullanicilar>();
        }
    
        public int t_id { get; set; }
        public string t_adi { get; set; }
        public string t_createuser { get; set; }
        public System.DateTime t_createdate { get; set; }
        public bool t_aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<adm_kullanicilar> adm_kullanicilar { get; set; }
    }
}
