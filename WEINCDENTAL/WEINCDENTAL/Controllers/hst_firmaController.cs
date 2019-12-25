using System;
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
  //  [Authorize(Roles = "1,3,4,5")]
    [CustomAutAttributes]
    public class hst_firmaController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_firma
        public ActionResult Index()
        {
            return View(db.hst_firma.ToList());
        }

        // GET: hst_firma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_firma hst_firma = db.hst_firma.Find(id);
            if (hst_firma == null)
            {
                return HttpNotFound();
            }
            return View(hst_firma);
        }

        // GET: hst_firma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: hst_firma/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_fad,t_aktif")] hst_firma hst_firma)
        {
            if (ModelState.IsValid)
            {
                db.hst_firma.Add(hst_firma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hst_firma);
        }

        // GET: hst_firma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_firma hst_firma = db.hst_firma.Find(id);
            if (hst_firma == null)
            {
                return HttpNotFound();
            }
            return View(hst_firma);
        }

        // POST: hst_firma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_fad,t_aktif")] hst_firma hst_firma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hst_firma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hst_firma);
        }

        // GET: hst_firma/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_firma hst_firma = db.hst_firma.Find(id);
            if (hst_firma == null)
            {
                return HttpNotFound();
            }
            return View(hst_firma);
        }

        // POST: hst_firma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hst_firma hst_firma = db.hst_firma.Find(id);
            db.hst_firma.Remove(hst_firma);
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
