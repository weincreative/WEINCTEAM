using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;
using WEINCDENTAL.Security;

namespace WEINCDENTAL.Controllers
{
  //  [Authorize(Roles = "1,3,4,5")]
    public class View_HizmetDetayController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: View_HizmetDetay
        public ActionResult HizmetDetay()
        {
            return View(db.View_HizmetDetay.ToList());
        }
        [HttpGet]
        public PartialViewResult _DisHHareket(int id)
        {
            var hizhareket = db.View_HizmetDetay.Where(k => k.BasvuruId == id && k.HHareketAktif==true &&k.BasvuruAktif==true).ToList();
            return PartialView(hizhareket);
        }

        [HttpGet]
        public PartialViewResult _VezneHHareket(int id)
        {
            var hizhareket = db.View_HizmetDetay.Where(k => k.BasvuruId == id && k.HHareketAktif == true && k.t_yapildi==true &&  k.BasvuruAktif == true).ToList();
            return PartialView(hizhareket);
        }


        public PartialViewResult _TEKDisHHareket(string tc,int dis)
        {
            var hizhareket = db.View_HizmetDetay.Where(k => k.TC == tc && k.DisNo == dis && k.HHareketAktif == true).ToList();
            ViewBag.DisNumarasi = dis;
            return PartialView(hizhareket);
        }

        [HttpPost]
        [CustomAutAttributes]
        public JsonResult Delete(int id)
        {
          
            var sonuc = 0;
            string msg="";
            var result = new HataMessage
            {
                sonuc = sonuc,
                message = msg
            };
            try
            {
               // hst_his_hareket hhareket = db.hst_his_hareket.Find(id);
                //hhareket.t_aktif = false;
                //db.Entry(hhareket).State = EntityState.Modified;
                //db.SaveChanges();
               db.sp_DeleteHhareket(id);
                
                result.sonuc = 1;
                result.message = "Silme İşlemi Başarılı Oldu.";
             
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = ex.InnerException.Message;
                result.message = msg;
                return Json(result, JsonRequestBehavior.AllowGet);
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
