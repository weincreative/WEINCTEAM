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
    using System.ComponentModel.DataAnnotations;

    public partial class hst_hastakarti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hst_hastakarti()
        {
            this.adm_pacs = new HashSet<adm_pacs>();
            this.hst_basvuru = new HashSet<hst_basvuru>();
            this.hst_hastadurum = new HashSet<hst_hastadurum>();
            this.hst_randevu = new HashSet<hst_randevu>();
        }
        [Display(Name = "Hasta ID")]
        [Required]
        public int t_id { get; set; }
        [Display(Name = "Hasta T.C.")]
        [Required]
        public string t_tc { get; set; }
        [Display(Name = "Hasta Ad�")]
        [Required]
        public string t_adi { get; set; }
        [Display(Name = "Hasta Soyad�")]
        [Required]
        public string t_soyadi { get; set; }
        [Display(Name = "Hasta Cinsiyet")]
        public int t_cinsiyet { get; set; }
        [Display(Name = "Hasta Medeni Durum")]
        public int t_medenidurum { get; set; }
        [Display(Name = "Hasta Do�um Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime t_dogumtarihi { get; set; }
        [Display(Name = "Hasta Do�um Yeri")]
        public string t_dogumyeri { get; set; }
        [Display(Name = "Hasta Telefon")]
        [Required]
        public string t_tel1 { get; set; }
        [Display(Name = "Hasta Telefon 2")]
        public string t_tel2 { get; set; }
        [Display(Name = "Hasta �lke")]
        public int t_ulkeId { get; set; }
        [Display(Name = "Hasta �l")]
        public Nullable<int> t_ilId { get; set; }
        [Display(Name = "Hasta �l�e")]
        public Nullable<int> t_ilceId { get; set; }
        [Display(Name = "Hasta Adres")]
        public string t_adres { get; set; }
        [Display(Name = "Olu�turan Kullanici")]
        [Required]
        public string t_createuser { get; set; }
        [Display(Name = "Olu�turulma Tarihi")]
        [Required]
        public System.DateTime t_createdate { get; set; }
        [Display(Name = "Aktif / Pasif")]
        public bool t_aktif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<adm_pacs> adm_pacs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_basvuru> hst_basvuru { get; set; }
        public virtual hst_cinsiyet hst_cinsiyet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_hastadurum> hst_hastadurum { get; set; }
        public virtual hst_il hst_il { get; set; }
        public virtual hst_ilce hst_ilce { get; set; }
        public virtual hst_medenidurum hst_medenidurum { get; set; }
        public virtual hst_ulke hst_ulke { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hst_randevu> hst_randevu { get; set; }
    }
}
