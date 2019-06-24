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
    public class hst_randevuController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_randevu
        public ActionResult Randevu_Index()
        {
            var hst_randevu = db.hst_randevu.Include(h => h.hst_hastakarti);
            return View(hst_randevu.ToList());
        }
        public ActionResult Randevular()
        {

            var list = db.hst_randevu.Where(k=>k.t_aktif==true).AsEnumerable().Select(e => new
            {
                id = e.t_id,
                title = e.t_tc,
                description = e.hst_hastakarti.t_adi + " " + e.hst_hastakarti.t_soyadi,
                start = e.t_baslangicsaat.ToString("MM/dd/yyyy HH:mm:ss"),
                end = e.t_bitissaat.ToString("MM/dd/yyyy HH:mm:ss"),
                allDay = e.allday,
                className = "event, bg-color-" + e.t_classname,
                icon = e.t_icon
            }).ToList();
           // var renk = "red";

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: hst_randevu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_randevu hst_randevu = db.hst_randevu.Find(id);
            if (hst_randevu == null)
            {
                return HttpNotFound();
            }
            return View(hst_randevu);
        }

        // GET: hst_randevu/Create
        public ActionResult Create()
        {
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi");
            return View();
        }

        // POST: hst_randevu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_basvuru,t_tc,t_baslangicsaat,t_bitissaat,t_classname,t_icon,allday,t_createuser,t_createdate,t_basvurudr,t_aktif")] hst_randevu hst_randevu)
        {
            if (ModelState.IsValid)
            {
                db.hst_randevu.Add(hst_randevu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_randevu.t_tc);
            return View(hst_randevu);
        }

        // GET: hst_randevu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_randevu hst_randevu = db.hst_randevu.Find(id);
            if (hst_randevu == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_randevu.t_tc);
            return View(hst_randevu);
        }

        // POST: hst_randevu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_basvuru,t_tc,t_baslangicsaat,t_bitissaat,t_classname,t_icon,allday,t_createuser,t_createdate,t_basvurudr,t_aktif")] hst_randevu hst_randevu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hst_randevu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_randevu.t_tc);
            return View(hst_randevu);
        }

        // GET: hst_randevu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_randevu hst_randevu = db.hst_randevu.Find(id);
            if (hst_randevu == null)
            {
                return HttpNotFound();
            }
            return View(hst_randevu);
        }

        // POST: hst_randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hst_randevu hst_randevu = db.hst_randevu.Find(id);
            db.hst_randevu.Remove(hst_randevu);
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
