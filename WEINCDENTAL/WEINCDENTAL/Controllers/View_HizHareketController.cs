using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;
using WEINCDENTAL.Security;

namespace WEINCDENTAL.Controllers
{
    //[Authorize(Roles = "1,3,4,5")]

    public class View_HizHareketController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: View_HizHareket
        public ActionResult Index()
        {
            return View(db.View_HizHareket.ToList());
        }
        [CustomAutAttributes]
        public PartialViewResult PartialHizHareket(int id)
        {
            
            var hizhareket = db.View_HizHareket.Where(k => k.basvuruid == id &&k.HHareketAkteif==true && k.Baktif==true).ToList();
            return PartialView(hizhareket);
        }

        [HttpPost]
        public JsonResult PlanlaYap(int id)
        {
            var sonuc = 0;
            try
            {
                 hst_his_hareket hHplanlamaYap = db.hst_his_hareket.Find(id);
                hHplanlamaYap.t_yapildi = true;
                db.Entry(hHplanlamaYap).State = EntityState.Modified;
                db.SaveChanges();
                sonuc = 1;
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult PlanlaYapma(int id)
        {
            var sonuc = 0;
            try
            {
                hst_his_hareket hHplanlamaYapma = db.hst_his_hareket.Find(id);
                hHplanlamaYapma.t_yapildi = false;
                db.Entry(hHplanlamaYapma).State = EntityState.Modified;
                db.SaveChanges();
                sonuc = 1;
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
