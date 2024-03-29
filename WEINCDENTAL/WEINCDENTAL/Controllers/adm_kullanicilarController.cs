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
    [CustomAutAttributes]
    public class adm_kullanicilarController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: adm_kullanicilar

        public ActionResult Index()
        {
            var adm_kullanicilar = db.adm_kullanicilar.Include(k=>k.adm_UserGroups);
            ViewBag.t_grup = new SelectList(db.adm_kullanicigrup, "t_id", "t_adi");
            return View(adm_kullanicilar.ToList());
        }
        
        // GET: adm_kullanicilar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_kullanicilar adm_kullanicilar = db.adm_kullanicilar.Find(id);
            if (adm_kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(adm_kullanicilar);
        }

        // GET: adm_kullanicilar/Create
        public ActionResult Create()
        {
            ViewBag.t_grup = new SelectList(db.adm_kullanicigrup.Where(k=>k.t_id!=1), "t_id", "t_adi");
            return View("Create", new adm_kullanicilar());
        }

        // POST: adm_kullanicilar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAutAttributes]
        public ActionResult Create([Bind(Include = "t_id,t_kodu,t_adi,t_sifre,t_grup,t_createuser,t_createdate,t_aktif")] adm_kullanicilar adm_kullanicilar)
        {
        
            adm_kullanicilar.t_createuser = System.Web.HttpContext.Current.User.Identity.Name;
            adm_kullanicilar.t_createdate = DateTime.Now;
            adm_kullanicilar.t_aktif = true;

            // mesaj=0 hiç bir uyarı yok.
            // mesaj=1 başarılı.
            // mesaj=2 başarısız.
            int mesaj = 0;
            if (ModelState.IsValid)
            {
                db.adm_kullanicilar.Add(adm_kullanicilar);
                db.SaveChanges();
                mesaj = 1;

                //  ViewBag.Message = mesaj;
                return RedirectToAction("KullaniciAyarlar", "Home", new { msjNo = mesaj });
            }

            ViewBag.t_grup = new SelectList(db.adm_kullanicigrup, "t_id", "t_adi", adm_kullanicilar.t_grup);
            return RedirectToAction("KullaniciAyarlar", "Home", new { msjNo = mesaj });
        }

        // GET: adm_kullanicilar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_kullanicilar adm_kullanicilar = db.adm_kullanicilar.Find(id);
            if (adm_kullanicilar == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_grup = new SelectList(db.adm_kullanicigrup, "t_id", "t_adi", adm_kullanicilar.t_grup);
            return View(adm_kullanicilar);
        }

        // POST: adm_kullanicilar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAutAttributes]
        public ActionResult Edit([Bind(Include = "t_id,t_kodu,t_adi,t_sifre,t_grup,t_yetki,t_createuser,t_createdate,t_aktif")] adm_kullanicilar adm_kullanicilar)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(adm_kullanicilar).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("KullaniciAyarlar", "Home");
            }
            ViewBag.t_grup = new SelectList(db.adm_kullanicigrup, "t_id", "t_adi", adm_kullanicilar.t_grup);
            ViewBag.t_yetki = new SelectList(db.adm_modulyetki, "t_id", "t_adi", adm_kullanicilar.t_yetki);
            return View(adm_kullanicilar);
        }

        // GET: adm_kullanicilar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_kullanicilar adm_kullanicilar = db.adm_kullanicilar.Find(id);
            if (adm_kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(adm_kullanicilar);
        }

        // POST: adm_kullanicilar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_kullanicilar adm_kullanicilar = db.adm_kullanicilar.Find(id);
            db.adm_kullanicilar.Remove(adm_kullanicilar);
            db.SaveChanges();
            return RedirectToAction("KullaniciAyarlar", "Home");
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
