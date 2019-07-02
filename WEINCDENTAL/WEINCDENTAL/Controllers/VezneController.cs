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
            if (vezne.Count != null)
            {
                decimal kalan = yc.GetKalanBorc(tc);
                decimal toptutar = yc.GetTopTutar(tc);
                decimal totalOdenen = yc.GetTotalOdenen(tc);
                ViewBag.TotalFiyat = toptutar;
                ViewBag.TotalKalan = kalan;
                ViewBag.TotalOdenen = totalOdenen;
            }
          


        
            return PartialView(vezne);
    }
    public PartialViewResult _CreateVezne(int id)
    {
        ViewBag.hhid = id;
        ViewBag.t_odemetipi = new SelectList(db.hst_odemetip.Where(k => k.t_Id != 3), "t_Id", "t_adi");
        //YardimciController yc = new YardimciController();
        //ViewBag.kalan = yc.GetKalanBorc(id);
        //ViewBag.totalodenen = yc.GetTotalOdenen(id);
        //ViewBag.Fiyat = db.View_Vezne.Where(k => k.HHareketAktif == true && k.Hareketid == id && k.VezneAktif == true)
        //    .Select(d => d.ToplamBorc).FirstOrDefault();

        return PartialView();
    }


    [HttpPost]
    public ActionResult Create([Bind(Include = "t_id,t_hareketid,t_odenen,t_kalan,t_odemetipi,t_createuser,t_odemetarih,t_createdate,t_aktif")] hst_vezne hst_vezne)
    {
        string msg = null;
        int drm = 0;
        //string htc = new YardimciController().HH_GetTC(hst_vezne.t_hareketid);
        decimal totalhizfiyat = Convert.ToDecimal(Request.Form["Fiyat"]);

        if (totalhizfiyat < hst_vezne.t_odenen)
        {
            msg = "Ödenmek istenen değer Toplam değerden fazla olamaz";
            drm = 1;
        }

        else if (hst_vezne.t_odenen < 0)
        {

            msg = "Yanlış değer girildi";
            drm = 1;
        }
        else
        {
            try
            {
                decimal kalan = totalhizfiyat - hst_vezne.t_odenen;
                hst_vezne.t_createuser = System.Web.HttpContext.Current.User.Identity.Name;
                hst_vezne.t_odemetarih = DateTime.Now;
                hst_vezne.t_createdate = DateTime.Now;
                hst_vezne.t_kalan = kalan;
                hst_vezne.t_aktif = true;
                db.hst_vezne.Add(hst_vezne);
                db.SaveChanges();
                drm = 2;
            }
            catch (Exception)
            {
                drm = 1;
                msg = "Vezne tablosunda bir hata meydana geldi";
            }
        }

        return RedirectToAction("SearchBorc", new {/* tc = htc,*/ emsg = msg, durum = drm });
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