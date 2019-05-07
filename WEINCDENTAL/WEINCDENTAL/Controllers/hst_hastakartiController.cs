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
    public class hst_hastakartiController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_hastakarti
        public ActionResult Index()
        {
            var hst_hastakarti = db.hst_hastakarti.Include(h => h.hst_cinsiyet).Include(h => h.hst_il).Include(h => h.hst_ilce).Include(h => h.hst_medenidurum);
            return View(hst_hastakarti.ToList());
        }

        // GET: hst_hastakarti/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hastakarti hst_hastakarti = db.hst_hastakarti.Find(id);
            if (hst_hastakarti == null)
            {
                return HttpNotFound();
            }
            return View(hst_hastakarti);
        }

        // GET: hst_hastakarti/Create
        public ActionResult Create()
        {
            ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi");
            ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi");
            ViewBag.t_ilceId = new SelectList(db.hst_ilce, "t_id", "t_adi");
            ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi");
            return View();
        }

        // POST: hst_hastakarti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_tc,t_adi,t_soyadi,t_cinsiyet,t_medenidurum,t_dogumtarihi,t_dogumyeri,t_tel1,t_tel2,t_ilId,t_ilceId,t_adres,t_createuser,t_createdate,t_aktif")] hst_hastakarti hst_hastakarti)
        {
            if (ModelState.IsValid)
            {
                db.hst_hastakarti.Add(hst_hastakarti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi", hst_hastakarti.t_cinsiyet);
            ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi", hst_hastakarti.t_ilId);
            ViewBag.t_ilceId = new SelectList(db.hst_ilce, "t_id", "t_adi", hst_hastakarti.t_ilceId);
            ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi", hst_hastakarti.t_medenidurum);
            return View(hst_hastakarti);
        }

        // GET: hst_hastakarti/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hastakarti hst_hastakarti = db.hst_hastakarti.Find(id);
            if (hst_hastakarti == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi", hst_hastakarti.t_cinsiyet);
            ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi", hst_hastakarti.t_ilId);
            ViewBag.t_ilceId = new SelectList(db.hst_ilce, "t_id", "t_adi", hst_hastakarti.t_ilceId);
            ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi", hst_hastakarti.t_medenidurum);
            return View(hst_hastakarti);
        }

        // POST: hst_hastakarti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_tc,t_adi,t_soyadi,t_cinsiyet,t_medenidurum,t_dogumtarihi,t_dogumyeri,t_tel1,t_tel2,t_ilId,t_ilceId,t_adres,t_createuser,t_createdate,t_aktif")] hst_hastakarti hst_hastakarti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hst_hastakarti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.t_cinsiyet = new SelectList(db.hst_cinsiyet, "t_id", "t_adi", hst_hastakarti.t_cinsiyet);
            ViewBag.t_ilId = new SelectList(db.hst_il, "t_id", "t_adi", hst_hastakarti.t_ilId);
            ViewBag.t_ilceId = new SelectList(db.hst_ilce, "t_id", "t_adi", hst_hastakarti.t_ilceId);
            ViewBag.t_medenidurum = new SelectList(db.hst_medenidurum, "t_id", "t_adi", hst_hastakarti.t_medenidurum);
            return View(hst_hastakarti);
        }

        // GET: hst_hastakarti/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hastakarti hst_hastakarti = db.hst_hastakarti.Find(id);
            if (hst_hastakarti == null)
            {
                return HttpNotFound();
            }
            return View(hst_hastakarti);
        }

        // POST: hst_hastakarti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            hst_hastakarti hst_hastakarti = db.hst_hastakarti.Find(id);
            db.hst_hastakarti.Remove(hst_hastakarti);
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
