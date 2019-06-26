using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEINCDENTAL.Models
{
    public class DTO_Vezne :WEINCDENTALEntities
    {
        public IEnumerable<WEINCDENTAL.Models.View_HizmetDetay> _ViewModelHDetay { get; set; }
        public IEnumerable<WEINCDENTAL.Models.View_Vezne> _ViewModelVezne { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalKalan { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalOdenen { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalFiyat { get; set; }
    }
}