using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEINCDENTAL.Models
{
    public class ViewModelAyarlar : WEINCDENTALEntities
    {
        public IEnumerable<WEINCDENTAL.Models.adm_kullanicilar> _ViewModelKullanicilar { get; set; }
        public IEnumerable<WEINCDENTAL.Models.adm_kullanicigrup> _ViewModelKullaniciGrup { get; set; }
        public IEnumerable<WEINCDENTAL.Models.adm_modulyetki> _ViewModelModulYetki { get; set; }
        public IEnumerable<WEINCDENTAL.Models.hst_hizmet> _ViewModelHizmet { get; set; }
    }
}