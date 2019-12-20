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
    public class adm_kullanicilar1Controller : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: adm_kullanicilar1
        public ActionResult Index()
        {
            return View(db.adm_kullanicilar.ToList());
        }

        // GET: adm_kullanicilar1/Details/5
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

        // GET: adm_kullanicilar1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_kullanicilar1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_kid,t_kodu,t_adi,t_sifre,t_grup,t_yetki,t_createuser,t_createdate,t_aktif")] adm_kullanicilar adm_kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.adm_kullanicilar.Add(adm_kullanicilar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adm_kullanicilar);
        }

        // GET: adm_kullanicilar1/Edit/5
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
            return View(adm_kullanicilar);
        }

        // POST: adm_kullanicilar1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_kid,t_kodu,t_adi,t_sifre,t_grup,t_yetki,t_createuser,t_createdate,t_aktif")] adm_kullanicilar adm_kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_kullanicilar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adm_kullanicilar);
        }

        // GET: adm_kullanicilar1/Delete/5
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

        // POST: adm_kullanicilar1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_kullanicilar adm_kullanicilar = db.adm_kullanicilar.Find(id);
            db.adm_kullanicilar.Remove(adm_kullanicilar);
            db.SaveChanges();
            return RedirectToAction("Index");
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
