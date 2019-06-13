using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEINCDENTAL.Models
{
    public class DTO_Vezne 
    {
        public int HizHareketId { get; set; }
        public int BasvuruId { get; set; }
        public int HizmetKod { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public System.DateTime DTarih { get; set; }
        public System.DateTime Basvuru_Tarih { get; set; }
        public string DoktorAd { get; set; }
        public string HizmetAd { get; set; }
        public decimal t_fiyat { get; set; }
        public bool Odemedurumu { get; set; }
        public bool t_borcdurum { get; set; }
        public System.DateTime Hizmet_Tarih { get; set; }
        public decimal t_odenen { get; set; }
        public decimal t_kalan { get; set; }
        public string Odemetip { get; set; }
        public System.DateTime Odemetarih { get; set; }
        public bool BasvuruAktif { get; set; }
        public bool HHareketAktif { get; set; }
    }
}