using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class YardimciController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // T.C. Sorgulama Hasta Kartı..
        [HttpPost]
        public ActionResult GetTc(string tc)
        {
            var varmi = db.hst_hastakarti.Count(k => k.t_aktif == true && k.t_tc == tc) > 0 ? true : false; ;

            if (varmi)
            {
                return RedirectToAction("Details", "hst_hastakarti", new { @id = tc });
            }
            else
            {
                return RedirectToAction("Create","hst_hastakarti");
            }

        }
    }
}