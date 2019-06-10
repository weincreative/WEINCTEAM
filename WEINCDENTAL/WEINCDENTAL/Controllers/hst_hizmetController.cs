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
    public class hst_hizmetController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_hizmet
        public ActionResult Index()
        {
            var hst_hizmet = db.hst_hizmet.Include(h => h.hst_cene_uygunmu).Include(h => h.hst_hizmet_parca);
            return View(hst_hizmet.ToList());
        }

        public PartialViewResult ListHizmet()
        {
            var hst_hizmet = db.hst_hizmet.Where(k=>k.t_aktif==true);
            var tc = Ortak._hastatc;
            int countHizmet = hst_hizmet.Count();
            ViewBag.tc = tc;
            ViewBag.HizmetCount = countHizmet;
            YardimciController yc=new YardimciController();
            int countOzelDurum = yc.GetOzelDurumCount(tc);
            ViewBag.OzelCount = countOzelDurum;
            return PartialView(hst_hizmet.ToList());
        }


        // GET: hst_hizmet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hizmet hst_hizmet = db.hst_hizmet.Find(id);
            if (hst_hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hst_hizmet);
        }

        // GET: hst_hizmet/Create
        public ActionResult Create()
        {
            ViewBag.t_ceneuygunmu = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi");
            ViewBag.t_parcauygunmu = new SelectList(db.hst_hizmet_parca, "t_id", "t_adi");
            return View();
        }

        // POST: hst_hizmet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_adi,t_parcauygunmu,t_ceneuygunmu,t_fiyat,t_createuser,t_createdate,t_aktif")] hst_hizmet hst_hizmet)
        {
            if (ModelState.IsValid)
            {
                db.hst_hizmet.Add(hst_hizmet);
                db.SaveChanges();
                return RedirectToAction("Ayarlar", "Home");
            }

            ViewBag.t_ceneuygunmu = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi", hst_hizmet.t_ceneuygunmu);
            ViewBag.t_parcauygunmu = new SelectList(db.hst_hizmet_parca, "t_id", "t_adi", hst_hizmet.t_parcauygunmu);
            return View(hst_hizmet);
        }

        // GET: hst_hizmet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hizmet hst_hizmet = db.hst_hizmet.Find(id);
            if (hst_hizmet == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_ceneuygunmu = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi", hst_hizmet.t_ceneuygunmu);
            ViewBag.t_parcauygunmu = new SelectList(db.hst_hizmet_parca, "t_id", "t_adi", hst_hizmet.t_parcauygunmu);
            return View(hst_hizmet);
        }

        // POST: hst_hizmet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_adi,t_parcauygunmu,t_ceneuygunmu,t_fiyat,t_createuser,t_createdate,t_aktif")] hst_hizmet hst_hizmet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hst_hizmet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Ayarlar", "Home");
            }
            ViewBag.t_ceneuygunmu = new SelectList(db.hst_cene_uygunmu, "t_id", "t_adi", hst_hizmet.t_ceneuygunmu);
            ViewBag.t_parcauygunmu = new SelectList(db.hst_hizmet_parca, "t_id", "t_adi", hst_hizmet.t_parcauygunmu);
            return View(hst_hizmet);
        }

        // GET: hst_hizmet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hizmet hst_hizmet = db.hst_hizmet.Find(id);
            if (hst_hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hst_hizmet);
        }

        // POST: hst_hizmet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hst_hizmet hst_hizmet = db.hst_hizmet.Find(id);
            db.hst_hizmet.Remove(hst_hizmet);
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
