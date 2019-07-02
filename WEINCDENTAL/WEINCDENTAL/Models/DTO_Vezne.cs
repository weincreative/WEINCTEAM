using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEINCDENTAL.Models
{
    public class DTO_Vezne:WEINCDENTALEntities
    {

        public IEnumerable<WEINCDENTAL.Models.View_BsvrVezne> _ViewModelVezne { get; set; }
        public IEnumerable<WEINCDENTAL.Models.View_HizmetDetay> _ViewModelHizmet { get; set; }
    }
}