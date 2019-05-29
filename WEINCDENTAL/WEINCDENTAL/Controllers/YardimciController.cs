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
            Ortak._hastatc = tc;
            var varmi = db.hst_hastakarti.Count(k => k.t_aktif == true && k.t_tc == tc) > 0 ? true : false; ;

            if (varmi)
            {
                return RedirectToAction("Hastabasvuru_Index", "hst_basvuru", new { @id = tc });
            }
            else
            {
                return RedirectToAction("Create","hst_hastakarti");
            }

        //            [HttpPost]
        //public ActionResult Save(EmployeeModel objSave)
        //{

        //    ViewBag.Msg = "Details saved successfully.";
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Draft(EmployeeModel objDraft)
        //{
        //    ViewBag.Msg = "Details saved as draft.";
        //    return View();
        //}

        //    [HttpPost]
        //[CokluButon(Argument = "btnA", Name = "action")]
        //public ActionResult AAction()
        //{
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //[CokluButon(Argument = "btnB", Name = "action")]
        //public ActionResult BAction()
        //{
        //    return RedirectToAction("Index");
        //}



    }

        // hastanın özel durum sayısını getirir...
        public int GetOzelDurumCount(string id)
        {
            var sonuc = 0;
            try
            {
               sonuc= db.View_HastalikDurum.Count(k => k.t_aktif == true && k.t_tc == id);
                return sonuc;
            }
            catch (Exception e)
            {
                return sonuc;
            }
           
           
        }

    }
}