using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{

    public class IstatistikController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        [HttpGet]
        public ActionResult Istatisitik()
        {
            return View();
        }

        
        public PartialViewResult _Hizmet(string baslangic, string bitis)
        {
            var ilkTarih = Convert.ToDateTime(baslangic);
            var sonTarih = Convert.ToDateTime(bitis);


            var hizmet = new YardimciController().GetHizmetList(ilkTarih, sonTarih);
            return PartialView(hizmet);
        }

    }
}