using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class View_HizmetDetayController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: View_HizmetDetay
        public ActionResult HizmetDetay()
        {
            return View(db.View_HizmetDetay.ToList());
        }

        public PartialViewResult _DisHHareket(int id)
        {
            var hizhareket = db.View_HizmetDetay.Where(k => k.BasvuruId == id && k.HHareketAktif==true).ToList();
            return PartialView(hizhareket);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var sonuc = 0;
            try
            {
                hst_his_hareket hhareket = db.hst_his_hareket.Find(id);
                hhareket.t_aktif = false;
                db.Entry(hhareket).State = EntityState.Modified;
                db.SaveChanges();
                sonuc = 1;
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
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
