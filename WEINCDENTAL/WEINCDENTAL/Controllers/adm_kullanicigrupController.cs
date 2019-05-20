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
    public class adm_kullanicigrupController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: adm_kullanicigrup
        public ActionResult KullaniciGrup_Index()
        {
            return View(db.adm_kullanicigrup.ToList());
        }

        // GET: adm_kullanicigrup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_kullanicigrup adm_kullanicigrup = db.adm_kullanicigrup.Find(id);
            if (adm_kullanicigrup == null)
            {
                return HttpNotFound();
            }
            return View(adm_kullanicigrup);
        }

        // GET: adm_kullanicigrup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_kullanicigrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_adi,t_createuser,t_createdate,t_aktif")] adm_kullanicigrup adm_kullanicigrup)
        {
            if (ModelState.IsValid)
            {
                db.adm_kullanicigrup.Add(adm_kullanicigrup);
                db.SaveChanges();
                return RedirectToAction("Ayarlar", "Home");
            }

            return View(adm_kullanicigrup);
        }

        // GET: adm_kullanicigrup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_kullanicigrup adm_kullanicigrup = db.adm_kullanicigrup.Find(id);
            if (adm_kullanicigrup == null)
            {
                return HttpNotFound();
            }
            return View(adm_kullanicigrup);
        }

        // POST: adm_kullanicigrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_adi,t_createuser,t_createdate,t_aktif")] adm_kullanicigrup adm_kullanicigrup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_kullanicigrup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Ayarlar", "Home");
            }
            return View(adm_kullanicigrup);
        }

        // GET: adm_kullanicigrup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_kullanicigrup adm_kullanicigrup = db.adm_kullanicigrup.Find(id);
            if (adm_kullanicigrup == null)
            {
                return HttpNotFound();
            }
            return View(adm_kullanicigrup);
        }

        // POST: adm_kullanicigrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_kullanicigrup adm_kullanicigrup = db.adm_kullanicigrup.Find(id);
            db.adm_kullanicigrup.Remove(adm_kullanicigrup);
            db.SaveChanges();
            return RedirectToAction("Ayarlar", "Home");
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
