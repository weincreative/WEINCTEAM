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
    public class hst_his_hareketController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_his_hareket
        public ActionResult Index()
        {
            var hst_his_hareket = db.hst_his_hareket.Include(h => h.hst_basvuru).Include(h => h.hst_cene_uygunmu).Include(h => h.hst_firma).Include(h => h.hst_hizmet);
            return View(hst_his_hareket.ToList());
        }

        // GET: hst_his_hareket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_his_hareket hst_his_hareket = db.hst_his_hareket.Find(id);
            if (hst_his_hareket == null)
            {
                return HttpNotFound();
            }
            return View(hst_his_hareket);
        }

        // GET: hst_his_hareket/Create
        public ActionResult Create()
        {
            ViewBag.t_basvuruid = new SelectList(db.hst_basvuru, "t_id", "t_basvuru");
            ViewBag.t_cene = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi");
            ViewBag.t_firmaid = new SelectList(db.hst_firma, "t_id", "t_fad");
            ViewBag.t_hizmetkodu = new SelectList(db.hst_hizmet, "t_id", "t_adi");
            return View();
        }

        // POST: hst_his_hareket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_basvuruid,t_hizmetkodu,t_islemtarihi,t_diskodu,t_parca,t_cene,t_yetiskinmi,t_odemevarmi,t_firmaid,t_createuser,t_createdate,t_aktif")] hst_his_hareket hst_his_hareket)
        {
            if (ModelState.IsValid)
            {
                db.hst_his_hareket.Add(hst_his_hareket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.t_basvuruid = new SelectList(db.hst_basvuru, "t_id", "t_basvuru", hst_his_hareket.t_basvuruid);
            ViewBag.t_cene = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi", hst_his_hareket.t_cene);
            ViewBag.t_firmaid = new SelectList(db.hst_firma, "t_id", "t_fad", hst_his_hareket.t_firmaid);
            ViewBag.t_hizmetkodu = new SelectList(db.hst_hizmet, "t_id", "t_adi", hst_his_hareket.t_hizmetkodu);
            return View(hst_his_hareket);
        }

        // GET: hst_his_hareket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_his_hareket hst_his_hareket = db.hst_his_hareket.Find(id);
            if (hst_his_hareket == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_basvuruid = new SelectList(db.hst_basvuru, "t_id", "t_basvuru", hst_his_hareket.t_basvuruid);
            ViewBag.t_cene = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi", hst_his_hareket.t_cene);
            ViewBag.t_firmaid = new SelectList(db.hst_firma, "t_id", "t_fad", hst_his_hareket.t_firmaid);
            ViewBag.t_hizmetkodu = new SelectList(db.hst_hizmet, "t_id", "t_adi", hst_his_hareket.t_hizmetkodu);
            return View(hst_his_hareket);
        }

        // POST: hst_his_hareket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_basvuruid,t_hizmetkodu,t_islemtarihi,t_diskodu,t_parca,t_cene,t_yetiskinmi,t_odemevarmi,t_firmaid,t_createuser,t_createdate,t_aktif")] hst_his_hareket hst_his_hareket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hst_his_hareket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.t_basvuruid = new SelectList(db.hst_basvuru, "t_id", "t_basvuru", hst_his_hareket.t_basvuruid);
            ViewBag.t_cene = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi", hst_his_hareket.t_cene);
            ViewBag.t_firmaid = new SelectList(db.hst_firma, "t_id", "t_fad", hst_his_hareket.t_firmaid);
            ViewBag.t_hizmetkodu = new SelectList(db.hst_hizmet, "t_id", "t_adi", hst_his_hareket.t_hizmetkodu);
            return View(hst_his_hareket);
        }

        // GET: hst_his_hareket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_his_hareket hst_his_hareket = db.hst_his_hareket.Find(id);
            if (hst_his_hareket == null)
            {
                return HttpNotFound();
            }
            return View(hst_his_hareket);
        }

        // POST: hst_his_hareket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hst_his_hareket hst_his_hareket = db.hst_his_hareket.Find(id);
            db.hst_his_hareket.Remove(hst_his_hareket);
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
