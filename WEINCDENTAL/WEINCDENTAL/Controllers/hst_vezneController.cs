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
    [Authorize(Roles = "1,3,4,5")]
    public class hst_vezneController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_vezne
        public ActionResult Index()
        {
            var hst_vezne = db.hst_vezne.Include(h => h.hst_his_hareket).Include(h => h.hst_odemetip);
            return View(hst_vezne.ToList());
        }

        // GET: hst_vezne/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_vezne hst_vezne = db.hst_vezne.Find(id);
            if (hst_vezne == null)
            {
                return HttpNotFound();
            }
            return View(hst_vezne);
        }

        // GET: hst_vezne/Create
        public ActionResult Create()
        {
            ViewBag.t_hareketid = new SelectList(db.hst_his_hareket, "t_id", "t_createuser");
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip, "t_Id", "t_adi");
            return View();
        }

        // POST: hst_vezne/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_hareketid,t_odenen,t_kalan,t_odemetipi,t_createuser,t_odemetarih,t_createdate,t_aktif")] hst_vezne hst_vezne)
        {
            if (ModelState.IsValid)
            {
                db.hst_vezne.Add(hst_vezne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.t_hareketid = new SelectList(db.hst_his_hareket, "t_id", "t_createuser", hst_vezne.t_hareketid);
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip, "t_Id", "t_adi", hst_vezne.t_odemetipi);
            return View(hst_vezne);
        }

        // GET: hst_vezne/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_vezne hst_vezne = db.hst_vezne.Find(id);
            if (hst_vezne == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_hareketid = new SelectList(db.hst_his_hareket, "t_id", "t_createuser", hst_vezne.t_hareketid);
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip, "t_Id", "t_adi", hst_vezne.t_odemetipi);
            return View(hst_vezne);
        }

        // POST: hst_vezne/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_hareketid,t_odenen,t_kalan,t_odemetipi,t_createuser,t_odemetarih,t_createdate,t_aktif")] hst_vezne hst_vezne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hst_vezne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.t_hareketid = new SelectList(db.hst_his_hareket, "t_id", "t_createuser", hst_vezne.t_hareketid);
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip, "t_Id", "t_adi", hst_vezne.t_odemetipi);
            return View(hst_vezne);
        }

        // GET: hst_vezne/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_vezne hst_vezne = db.hst_vezne.Find(id);
            if (hst_vezne == null)
            {
                return HttpNotFound();
            }
            return View(hst_vezne);
        }

        // POST: hst_vezne/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hst_vezne hst_vezne = db.hst_vezne.Find(id);
            db.hst_vezne.Remove(hst_vezne);
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
