//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace WEINCDENTAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class View_HizmetDetay
    {
        public int HizHareketId { get; set; }
        public int BasvuruId { get; set; }
        public int HizmetKod { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public System.DateTime DTarih { get; set; }
        public string Tel { get; set; }
        public System.DateTime Basvuru_Tarih { get; set; }
        public string DoktorAd { get; set; }
        public bool t_taburcu { get; set; }
        public string HizmetAd { get; set; }
        [DataType(DataType.Currency)]
        public decimal t_fiyat { get; set; }
        public bool Odemedurumu { get; set; }
        public System.DateTime Hizmet_Tarih { get; set; }
        public int DisNo { get; set; }
        public int CeneNo { get; set; }
        public string CeneAdi { get; set; }
        public bool YetiskinMi { get; set; }
        public bool DoktorAktif { get; set; }
        public bool BasvuruAktif { get; set; }
        public bool HHareketAktif { get; set; }
        public bool t_borcdurum { get; set; }
        public string t_fad { get; set; }
        public string t_mad { get; set; }
    }
}
