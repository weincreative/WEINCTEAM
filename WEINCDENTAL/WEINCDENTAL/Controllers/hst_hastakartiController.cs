﻿using System;
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
    //[CustomAutAttributes]
    public class hst_hastakartiController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        [CustomAutAttributes]
        // GET: hst_hastakarti
        public ActionResult Hastakarti_Index()
        {
            var uid = Convert.ToInt32(Session["userId"]);
            TokenController tokenController = new TokenController();
            var PacsYetki = tokenController.GetYetkis(uid, "Pacs", "_PartialPacsList");
            ViewBag.Pacs = PacsYetki;

            var hst_hastakarti = db.hst_hastakarti.Include(h => h.hst_cinsiyet).Include(h => h.hst_il).Include(h => h.hst_ilce).Include(h => h.hst_medenidurum).Include(h => h.hst_ulke).Where(k => k.t_aktif == true);
            return View(hst_hastakarti.ToList());
        }

        //İle göre ilçe getir...
        [HttpPost]
        public JsonResult GetIlIlce(int id)
        {
            List<hst_ilce> towns = null;
            towns = db.hst_ilce.Where(k => k.t_aktif == true && k.t_ilId == id).OrderBy(a => a.t_adi).ToList();
            List<SelectListItem> itemList = (from i in towns
                                             select new SelectListItem
                                             {
                                                 Value = i.t_id.ToString(),
                                                 Text = i.t_adi
                                             }).ToList();

            return Json(new { sonuc = itemList, JsonRequestBehavior.AllowGet });
        }


        public PartialViewResult GetHastaBilgi(string id)
        {
            //id = "12312312121";
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hastakarti hst_hastakarti = db.hst_hastakarti.Find(id);
            if (hst_hastakarti == null)
            {
                return PartialView();
                // return HttpNotFound();
            }
            var ad_soyad = hst_hastakarti.t_adi + " " + hst_hastakarti.t_soyadi;
            ViewBag.Ad = ad_soyad;

            Ortak._hastayas = new YardimciController().GetYas(id);

            return PartialView(hst_hastakarti);
        }

        [CustomAutAttributes]
        // GET: hst_hastakarti/Create
        public ActionResult Create()
        {
         
            try
            {
                string id = Ortak._hastatc;
                ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi");
                ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi");
                ViewBag.t_ilceId = new SelectList(db.hst_ilce, "t_id", "t_adi");
                ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi");
                List<hst_ulke> ulke = null;
                ulke = db.hst_ulke.Where(k => k.Aktif == true).ToList();
                List<SelectListItem> itemList = (from i in ulke
                                                 select new SelectListItem
                                                 {
                                                     Value = i.CountryID.ToString(),
                                                     Text = i.CountryName,
                                                     Selected = i.CountryID == 212
                                                 }).ToList();

                // ViewBag.t_ulkeId = new SelectList(db.hst_ulke, "CountryID", "CountryName");
                ViewBag.t_ulkeId = itemList;
                ViewBag.tc = id;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create", "hst_hastakarti");
            }

        }

        // POST: hst_hastakarti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAutAttributes]
        public ActionResult Create([Bind(Include = "t_id,t_tc,t_adi,t_soyadi,t_cinsiyet,t_medenidurum,t_dogumtarihi,t_dogumyeri,t_tel1,t_tel2,t_ulkeId,t_ilId,t_ilceId,t_adres,t_createuser,t_createdate,t_aktif,yabancimi,t_dosyano")] hst_hastakarti hst_hastakarti)
        {
            try
            {
                var yabanci = hst_hastakarti.yabancimi;

                // yabancı uyruklu checkbox işaretli ise..
                if (yabanci)
                {
                    hst_hastakarti.t_ilId = null;
                    hst_hastakarti.t_ilceId = null;

                }
                else
                {
                    hst_hastakarti.t_ulkeId = 212;
                }

                // mesaj=0 hiç bir uyarı yok.
                // mesaj=1 başarılı.
                // mesaj=2 başarısız.
                int mesaj = 0;
                List<hst_ulke> country = null;
                country = db.hst_ulke.Where(k => k.Aktif == true).ToList();
                List<SelectListItem> itemList = (from i in country
                                                 select new SelectListItem
                                                 {
                                                     Value = i.CountryID.ToString(),
                                                     Text = i.CountryName,
                                                     Selected = i.CountryID == 212
                                                 }).ToList();
                ViewBag.t_ulkeId = itemList;
                if (ModelState.IsValid)
                {
                    hst_hastakarti.t_createdate = DateTime.Now;
                    hst_hastakarti.t_createuser = System.Web.HttpContext.Current.User.Identity.Name;
                    hst_hastakarti.t_aktif = true;

                    db.hst_hastakarti.Add(hst_hastakarti);
                    db.SaveChanges();
                    // return View();
                    mesaj = 1;

                    ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi", hst_hastakarti.t_cinsiyet);
                    ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi", hst_hastakarti.t_ilId);
                    ViewBag.t_ilceId = new SelectList(db.hst_ilce, "t_id", "t_adi", hst_hastakarti.t_ilceId);
                    ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi", hst_hastakarti.t_medenidurum);
                    ViewBag.Message = mesaj;
                    Ortak._hastatc = hst_hastakarti.t_tc;
                    return RedirectToAction("HastaBasvuruCreate", "hst_basvuru", new { id = hst_hastakarti.t_tc });
                }
                mesaj = 2;
                ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi", hst_hastakarti.t_cinsiyet);
                ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi", hst_hastakarti.t_ilId);
                ViewBag.t_ilceId = new SelectList(db.hst_ilce, "t_id", "t_adi", hst_hastakarti.t_ilceId);
                ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi", hst_hastakarti.t_medenidurum);
                ViewBag.Message = mesaj;
                return View(hst_hastakarti);

            }
            catch (Exception ex)
            {
                //return Content("<script language='javascript' type='text/javascript'>console.log('Hasta Kartını KAYDEDEMEDİM MK ');</script>");
                //ViewBag.DynamicScripts = "console.log('hatalikayit LAA');";
                //Response.Write("<script>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
                //ViewBag.HataMessage = ex.Message;
                return RedirectToAction("Create", "hst_hastakarti");


                //location.href = '@Url.Action("ayarlar", "home")'"
            }

        }

        // GET: hst_hastakarti/Edit/5
        [CustomAutAttributes]
        public ActionResult HastaEdit(string tc)
        {
            
            hst_hastakarti hst_hastakarti = db.hst_hastakarti.Find(tc);
            if (hst_hastakarti == null)
            {

            }
            ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi", hst_hastakarti.t_cinsiyet);
            ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi", hst_hastakarti.t_ilId);

            List<SelectListItem> itemList = (from i in db.hst_ilce.Where(k => k.t_aktif == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Value = i.t_id.ToString(),
                                                 Text = i.t_adi,
                                                 Selected = i.t_id == hst_hastakarti.t_ilceId
                                             }).ToList();

            ViewBag.t_ilceId2 = itemList;
            ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi", hst_hastakarti.t_medenidurum);
            ViewBag.t_ulkeId = new SelectList(db.hst_ulke, "CountryID", "BinaryCode", hst_hastakarti.t_ulkeId);
            return View(hst_hastakarti);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAutAttributes]
        public ActionResult HastaEdit([Bind(Include = "yabancimi,t_id,t_tc,t_adi,t_soyadi,t_cinsiyet,t_medenidurum,t_dogumtarihi,t_dogumyeri,t_tel1,t_tel2,t_ulkeId,t_ilId,t_ilceId,t_adres,t_createuser,t_createdate,t_aktif,t_dosyano")] hst_hastakarti hst_hastakarti)
        {
            try
            {
                var yabanci = hst_hastakarti.yabancimi;
                // yabancı uyruklu checkbox işaretli ise..
                if (yabanci)
                {
                    hst_hastakarti.t_ilId = null;
                    hst_hastakarti.t_ilceId = null;

                }
                else
                {
                    hst_hastakarti.t_ulkeId = 212;
                }


                if (ModelState.IsValid)
                {
                    hst_hastakarti.t_createdate = DateTime.Now;
                    hst_hastakarti.t_createuser = System.Web.HttpContext.Current.User.Identity.Name;
                    hst_hastakarti.t_aktif = true;
                    db.Entry(hst_hastakarti).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Hastakarti_Index");
                }
                ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi", hst_hastakarti.t_cinsiyet);
                ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi", hst_hastakarti.t_ilId);
                List<SelectListItem> itemList = (from i in db.hst_ilce.Where(k => k.t_aktif == true).ToList()
                                                 select new SelectListItem
                                                 {
                                                     Value = i.t_id.ToString(),
                                                     Text = i.t_adi,
                                                     Selected = i.t_id == hst_hastakarti.t_ilceId
                                                 }).ToList();

                ViewBag.t_ilceId2 = itemList;
                ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi", hst_hastakarti.t_medenidurum);
                ViewBag.t_ulkeId = new SelectList(db.hst_ulke, "CountryID", "BinaryCode", hst_hastakarti.t_ulkeId);
                return View(hst_hastakarti);
            }
            catch (Exception ex)
            {
                return View(hst_hastakarti);
            }
        }


        // POST: hst_hastakarti/Delete/5
        [HttpPost]
        public JsonResult Delete(string tc)
        {
            var sonuc = 0;
            try
            {
                hst_hastakarti hastakartiRem = db.hst_hastakarti.Find(tc);
                hastakartiRem.t_aktif = false;
                db.Entry(hastakartiRem).State = EntityState.Modified;
                db.SaveChanges();
                sonuc = 1;
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
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
