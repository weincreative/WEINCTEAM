using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    [Authorize(Roles = "1,3,4")]
    public class VezneController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();


        // GET: Vezne
        public ActionResult SearchBorc(string tc, string emsg, int? durum)
        {
            ViewBag.tc = tc;

            if (emsg != null)
            {
                ViewBag.hata = durum;
                ViewBag.error = emsg;
            }

            return View();
        }

        [HttpGet]
        public PartialViewResult _Vezne(string tc)
        {

            YardimciController yc = new YardimciController();
            var vezne = db.View_BsvrVezne
                .Where(k => k.HastaAktif == true && k.BasvuruAktif == true && k.VezneAktif == true)
                .Where(p => p.t_tc == tc).ToList();
            if (vezne.Count != 0)
            {
                decimal kalan = yc.GetKalanBorc(tc);
                decimal toptutar = yc.GetTopTutar(tc);
                decimal totalOdenen = yc.GetTotalOdenen(tc);
                decimal totalindirim = yc.GetToplamIndirim(tc);
                decimal asilodenecek = toptutar - totalindirim;
                ViewBag.TotalFiyat = toptutar;
                ViewBag.Totalindirim = totalindirim;
                ViewBag.TotalKalan = kalan;
                ViewBag.AsilOdenecek = asilodenecek;
                ViewBag.TotalOdenen = totalOdenen;
            }




            return PartialView(vezne);
        }
        public PartialViewResult _CreateVezne(int id)
        {
            ViewBag.bid = id;
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip.Where(k => k.t_Id != 3), "t_Id", "t_adi");
            YardimciController yc = new YardimciController();
            ViewBag.kalan = yc.GetKalanBorc(id);
            ViewBag.totalodenen = yc.GetTotalOdenen(id);
            ViewBag.Fiyat = yc.GetTopTutar(id);
            ViewBag.totalindirim = yc.GetToplamIndirim(id);

            return PartialView();
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "t_id,t_bid,t_hizid,t_odenen,t_kalan,t_total,t_indirim,t_odenecek,t_odemetipi,t_createuser,t_odemetarih,t_createdate,t_aktif")] hst_vezne hst_vezne)
        {
            string msg = null;
            int drm = 0;
            string htc = new YardimciController().VH_GetTC(hst_vezne.t_bid);
            decimal totalhizfiyat = Convert.ToDecimal(Request.Form["Fiyat"]);
            decimal oncekiindirim = Convert.ToDecimal(Request.Form["totalindirim"]);
            decimal oncekiodenen = Convert.ToDecimal(Request.Form["totalodenen"]);

            decimal topIndirim = oncekiindirim + hst_vezne.t_indirim;
            decimal topodenecek = oncekiodenen + hst_vezne.t_odenen+topIndirim;
            if (totalhizfiyat < hst_vezne.t_odenen || totalhizfiyat < topodenecek)
            {
                msg = "Ödenmek istenen değer Toplam değerden fazla olamaz";
                drm = 1;
            }else if (totalhizfiyat<topIndirim)
            {
                msg = "İndirim değeri Toplam fiyattan fazla olamaz";
                drm = 1;
            }

            else if (hst_vezne.t_odenen < 0 || hst_vezne.t_indirim < 0)
            {

                msg = "Yanlış değer girildi";
                drm = 1;
            }
            else
            {
                try
                {
                    decimal kalan = totalhizfiyat - topodenecek;
                    hst_vezne.t_createuser = System.Web.HttpContext.Current.User.Identity.Name;
                    hst_vezne.t_odemetarih = DateTime.Now;
                    hst_vezne.t_createdate = DateTime.Now;
                    hst_vezne.t_kalan = kalan;
                    hst_vezne.t_aktif = true;
                    db.hst_vezne.Add(hst_vezne);
                    db.SaveChanges();
                    drm = 2;
                    msg = "Ödeme İşleminiz Başarılı Olmuştur.";
                }
                catch (Exception)
                {
                    drm = 1;
                    msg = "Vezne tablosunda bir hata meydana geldi";
                }
            }

            return RedirectToAction("SearchBorc", new { tc = htc, emsg = msg, durum = drm });
        }

        [HttpPost]
        public JsonResult Delete(int vid)
        {
            var sonuc = 0;
            try
            {
                hst_vezne vezne = db.hst_vezne.Find(vid);
                vezne.t_aktif = false;
                db.Entry(vezne).State = EntityState.Modified;
                db.SaveChanges();
                sonuc = 1;
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }


        }
    }
}