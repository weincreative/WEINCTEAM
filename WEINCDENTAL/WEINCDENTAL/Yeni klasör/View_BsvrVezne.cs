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
    
    public partial class View_BsvrVezne
    {
        public int v_id { get; set; }
        public int b_id { get; set; }
        public string t_tc { get; set; }
        public string t_adi { get; set; }
        public string t_soyadi { get; set; }
        public System.DateTime t_dogumtarihi { get; set; }
        public System.DateTime t_basvurutarihi { get; set; }
        public decimal t_odenen { get; set; }
        public decimal t_kalan { get; set; }
        public decimal AsilTutar { get; set; }
        public decimal t_indirim { get; set; }
        public decimal OdenecekTutar { get; set; }
        public string OdemeTip { get; set; }
        public System.DateTime t_odemetarih { get; set; }
        public bool BorcDurum { get; set; }
        public bool BasvuruAktif { get; set; }
        public bool VezneAktif { get; set; }
        public bool HastaAktif { get; set; }
        public Nullable<bool> HizmetYapildi { get; set; }
    }
}
