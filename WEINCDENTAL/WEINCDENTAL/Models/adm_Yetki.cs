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
    
    public partial class adm_Yetki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public adm_Yetki()
        {
            this.adm_YetkiGroups = new HashSet<adm_YetkiGroups>();
            this.adm_YetkiMethods = new HashSet<adm_YetkiMethods>();
        }
    
        public long Id { get; set; }
        public string YetkiAdi { get; set; }
        public string YetkiAciklama { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public bool Aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<adm_YetkiGroups> adm_YetkiGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<adm_YetkiMethods> adm_YetkiMethods { get; set; }
    }
}
