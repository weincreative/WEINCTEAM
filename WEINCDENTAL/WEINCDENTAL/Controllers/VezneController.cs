using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    [Authorize(Roles = "1,2")]
    public class VezneController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        // GET: Vezne
        public ActionResult SearchBorc(string tc,string msg)
        {

            ViewBag.tc = tc;
            if (msg!="")
            {
                ViewBag.error = msg;
            }
           
            return View();
        }

        public PartialViewResult _Vezne(string tc)
        {
            var borc = db.View_Vezne
                .Where(k => k.t_borcdurum == true && k.HHareketAktif == true && k.t_tc==tc && k.VezneAktif == true).ToList();
         
            YardimciController yc=new YardimciController();
            decimal kalan = yc.GetKalanBorc(tc);
            decimal toptutar = yc.GetTopTutar(tc);
            decimal totalOdenen = yc.GetTotalOdenen(tc);
            ViewBag.TotalFiyat = toptutar;
            ViewBag.TotalKalan = kalan;
            ViewBag.TotalOdenen = totalOdenen;
            return PartialView(borc);
        }
        public PartialViewResult _CreateVezne(int id)
        {
            ViewBag.hhid=id;
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip.Where(k=>k.t_Id!=3), "t_Id", "t_adi");
            YardimciController yc=new YardimciController();
            ViewBag.kalan = yc.GetKalanBorc(id);
            ViewBag.totalodenen = yc.GetTotalOdenen(id);
            ViewBag.Fiyat = db.View_Vezne.Where(k => k.HHareketAktif == true && k.Hareketid == id && k.VezneAktif==true)
                .Select(d => d.ToplamBorc).FirstOrDefault();

            return PartialView();
        }
        
        

        [HttpPost]
        public ActionResult Create([Bind(Include = "t_id,t_hareketid,t_odenen,t_kalan,t_odemetipi,t_createuser,t_odemetarih,t_createdate,t_aktif")] hst_vezne hst_vezne)
        {

            string mesaj = "";
            string htc=new YardimciController().HH_GetTC(hst_vezne.t_hareketid);
            decimal totalhizfiyat =Convert.ToDecimal(Request.Form["Fiyat"]);

            if (totalhizfiyat<hst_vezne.t_odenen)
            {
                mesaj = "Ödenmek istenen değer Toplam değerden fazla olamaz";
            }

            else if (hst_vezne.t_odenen<0)
            {
                mesaj = "Yanlış değer girildi";
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
                }
                catch (Exception)
                {
                    mesaj = "Vezne tablosunda bir hata meydana geldi";
                }
            }
                
            

            return RedirectToAction("SearchBorc",new{tc=htc,msg=mesaj});
        }
    }
}