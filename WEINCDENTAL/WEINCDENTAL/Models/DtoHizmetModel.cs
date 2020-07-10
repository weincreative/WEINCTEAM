using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEINCDENTAL.Models
{
    public class DtoHizmetModel
    {
        public string HizmetAd { get; set; }
        public int ToplamHizmet { get; set; }
        [DataType(DataType.Currency)]
        public decimal ToplamFiyat { get; set; }

    }
}