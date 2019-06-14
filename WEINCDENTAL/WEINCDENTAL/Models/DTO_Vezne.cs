using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEINCDENTAL.Models
{
    public class DTO_Vezne :WEINCDENTALEntities
    {
        public IEnumerable<WEINCDENTAL.Models.View_HizmetDetay> _ViewModelHDetay { get; set; }
        public IEnumerable<WEINCDENTAL.Models.View_Vezne> _ViewModelVezne { get; set; }
      
    }
}