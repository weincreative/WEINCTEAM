using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEINCDENTAL.Models
{
    public class ViewModelHisHareket: WEINCDENTALEntities
    {
        public IEnumerable<WEINCDENTAL.Models.hst_his_hareket>_ViewModelHisHareket { get; set; }
        public IEnumerable<WEINCDENTAL.Models.hst_hizmet> _ViewModelHizmet { get; set; }
        public IEnumerable<WEINCDENTAL.Models.hst_basvuru> _ViewModelBasvuru { get; set; }
        public IEnumerable<WEINCDENTAL.Models.hst_hastakarti> _ViewModelHastakarti { get; set; }
        public IEnumerable<WEINCDENTAL.Models.hst_hastadurum> _ViewModelHastaDurum { get; set; }
        public IEnumerable<WEINCDENTAL.Models.hst_firma> _ViewModelFirma { get; set; }
        public IEnumerable<WEINCDENTAL.Models.adm_pacs> _ViewModelPacs { get; set; }
    }
}