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

    public partial class View_HizHareket
    {
        public int basvuruid { get; set; }
        public string t_tc { get; set; }
        public string t_adi { get; set; }
        public string t_soyadi { get; set; }
        public string BasvuruNo { get; set; }
        public System.DateTime t_basvurutarihi { get; set; }
        public string Doktoradi { get; set; }
        public Nullable<int> t_basvurudr { get; set; }
        public bool t_taburcu { get; set; }
        public int Hareketid { get; set; }
        public int t_hizmetkodu { get; set; }
        public string Hizmetadi { get; set; }
        public bool t_yetiskinmi { get; set; }
        public int t_diskodu { get; set; }
        [DataType(DataType.Currency)]
        public decimal t_fiyat { get; set; }
        public System.DateTime t_islemtarihi { get; set; }
        public bool Odemeyapildimi { get; set; }
        public int CeneNo { get; set; }
        public string CeneDurum { get; set; }
        public bool HHareketAkteif { get; set; }
        public bool Baktif { get; set; }
        public bool HastaAktif { get; set; }
        public Nullable<bool> t_yapildi { get; set; }
    }
}