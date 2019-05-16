using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class YardimciController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        //  public static string _hastatc;


        // T.C. Sorgulama Hasta Kartı..
        [HttpPost]
        public ActionResult GetTc(string tc)

        {
            Ortak._hastatc = tc;

            var varmi = db.hst_hastakarti.Count(k => k.t_aktif == true && k.t_tc == tc) > 0 ? true : false; ;

            if (varmi)
            {
                return RedirectToAction("Details", "hst_hastakarti", new { @id = tc });
                //return RedirectToAction("Index", "hst_hastakarti");
            }
            else
            {
                return RedirectToAction("Create", "hst_hastakarti");
            }

        }


        
    }
}