using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Content
{
    public class adm_modulyetkiController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: adm_modulyetki
        public ActionResult Index()
        {
            return View(db.adm_modulyetki.ToList());
        }

        // GET: adm_modulyetki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_modulyetki adm_modulyetki = db.adm_modulyetki.Find(id);
            if (adm_modulyetki == null)
            {
                return HttpNotFound();
            }
            return View(adm_modulyetki);
        }

        // GET: adm_modulyetki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adm_modulyetki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_adi,t_createuser,t_createdate,t_aktif")] adm_modulyetki adm_modulyetki)
        {
            if (ModelState.IsValid)
            {
                db.adm_modulyetki.Add(adm_modulyetki);
                db.SaveChanges();
                return RedirectToAction("Ayarlar", "Home");
            }

            return View(adm_modulyetki);
        }

        // GET: adm_modulyetki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_modulyetki adm_modulyetki = db.adm_modulyetki.Find(id);
            if (adm_modulyetki == null)
            {
                return HttpNotFound();
            }
            return View(adm_modulyetki);
        }

        // POST: adm_modulyetki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_adi,t_createuser,t_createdate,t_aktif")] adm_modulyetki adm_modulyetki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adm_modulyetki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Ayarlar", "Home");
            }
            return View(adm_modulyetki);
        }

        // GET: adm_modulyetki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            adm_modulyetki adm_modulyetki = db.adm_modulyetki.Find(id);
            if (adm_modulyetki == null)
            {
                return HttpNotFound();
            }
            return View(adm_modulyetki);
        }

        // POST: adm_modulyetki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adm_modulyetki adm_modulyetki = db.adm_modulyetki.Find(id);
            db.adm_modulyetki.Remove(adm_modulyetki);
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
