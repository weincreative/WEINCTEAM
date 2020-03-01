using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEINCDENTAL.Models
{
    public class ModelIstatistik
    {
        [DataType(DataType.Currency)]
        public decimal? TotalGelir { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalNakit { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalKKart { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalOdenecek { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalOdenen { get; set; }
        public int? TotalHizmet { get; set; }
        public int? TotalHasta { get; set; }
        

    }
}