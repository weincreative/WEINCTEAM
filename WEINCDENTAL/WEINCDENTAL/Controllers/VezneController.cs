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
        public PartialViewResult _CreateVezne(int id,bool odurum)
        {
            ViewBag.hhid=id;
            ViewBag.odurum = odurum;
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip, "t_Id", "t_adi");
            YardimciController yc=new YardimciController();
            ViewBag.kalan = yc.GetKalanBorc(id);
            ViewBag.totalodenen = yc.GetTotalOdenen(id);
            ViewBag.Fiyat = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.HizHareketId == id)
                .Select(d => d.t_fiyat).FirstOrDefault();

            return PartialView();
        }
        
        

        [HttpPost]
        public ActionResult Create([Bind(Include = "t_id,t_hareketid,t_odenen,t_kalan,t_odemetipi,t_createuser,t_odemetarih,t_createdate,t_aktif")] hst_vezne hst_vezne)
        {

            string mesaj = "";
            string htc=new YardimciController().HH_GetTC(hst_vezne.t_hareketid);
            decimal totalhizfiyat =Convert.ToDecimal(Request.Form["Fiyat"]);
            bool odemedurum =Boolean.Parse(Request.Form["odurum"]);
            if (!odemedurum)
            {
                hst_his_hareket hst_his_hareket = db.hst_his_hareket.Find(hst_vezne.t_hareketid);
                if (hst_his_hareket == null)
                {
                    mesaj = "Kayıtlı Hizmet bulunamadı.";
                    // hata ver
                }
                else
                {
                    try
                    {
                        hst_his_hareket.t_odemevarmi = true;
                        hst_his_hareket.t_createdate = DateTime.Now;
                        hst_his_hareket.t_createuser = System.Web.HttpContext.Current.User.Identity.Name;
                        db.Entry(hst_his_hareket).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        mesaj = "Hizmet tablosunda bir hata meydana geldi. ";
                        //hata gonder
                    }

                }
            }
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
            catch (Exception )
            {
                mesaj = "Vezne tablosunda bir hata meydana geldi";
            }

            return RedirectToAction("SearchBorc",new{tc=htc,msg=mesaj});
        }
    }
}