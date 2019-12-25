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
    // [Authorize(Roles = "1,3,4,5")]
   // [CustomAutAttributes]
    public class hst_hastadurumController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();


        public PartialViewResult HastaDPartialCreate(string id)
        {

            ViewBag.t_tc = id;
            ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi");
            return PartialView();
        }

        // GET: hst_hastadurum/Create
        public ActionResult Create()
        {
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi");
            ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi");
            return View();
        }

        // POST: hst_hastadurum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_tc,t_hdurumid,t_aciklama,t_aktif")] hst_hastadurum hst_hastadurum)
        {
            int mesaj = 0;
            if (ModelState.IsValid)
            {

                hst_hastadurum.t_aktif = true;
                db.hst_hastadurum.Add(hst_hastadurum);
                db.SaveChanges();
                mesaj = 1;
                ViewBag.Message = mesaj;
                ViewBag.t_tc = hst_hastadurum.t_tc;
                return RedirectToAction("Index");
            }
            mesaj = 2;
            ViewBag.Message = mesaj;
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_hastadurum.t_tc);
            ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi", hst_hastadurum.t_hdurumid);
            return View(hst_hastadurum);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HDurumCreate([Bind(Include = "t_id,t_tc,t_hdurumid,t_aciklama,t_aktif")] hst_hastadurum hst_hastadurum)
        {
            int mesaj = 0;
            if (ModelState.IsValid)
            {

                hst_hastadurum.t_aktif = true;
                db.hst_hastadurum.Add(hst_hastadurum);
                db.SaveChanges();
                mesaj = 1;
                ViewBag.Message = mesaj;
                ViewBag.t_tc = hst_hastadurum.t_tc;
                return Json(mesaj,JsonRequestBehavior.AllowGet);
            }
            mesaj = 2;
            ViewBag.Message = mesaj;
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_hastadurum.t_tc);
            ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi", hst_hastadurum.t_hdurumid);
            return Json(mesaj, JsonRequestBehavior.AllowGet);
        }


        // GET: hst_hastadurum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hastadurum hst_hastadurum = db.hst_hastadurum.Find(id);
            if (hst_hastadurum == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_hastadurum.t_tc);
            ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi", hst_hastadurum.t_hdurumid);
            return View(hst_hastadurum);
        }



        public PartialViewResult HDurumPartialEdit(int? id)
        {
            //if (id == null)
            //{
            //  //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            hst_hastadurum hst_hastadurum = db.hst_hastadurum.Find(id);
            if (hst_hastadurum == null)
            {
                ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi");
                ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi");
                return PartialView(hst_hastadurum);
            }
            ViewBag.t_tc = hst_hastadurum.t_tc;
            ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi", hst_hastadurum.t_hdurumid);
            hst_hastadurum.t_aktif = !hst_hastadurum.t_aktif;
            return PartialView(hst_hastadurum);
        }

        // POST: hst_hastadurum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_tc,t_hdurumid,t_aciklama,t_aktif")] hst_hastadurum hst_hastadurum)
        {
            int mesaj = 0;
            if (ModelState.IsValid)
            {
                hst_hastadurum.t_aktif = !hst_hastadurum.t_aktif;
                db.Entry(hst_hastadurum).State = EntityState.Modified;
                db.SaveChanges();
                mesaj = 1;
                ViewBag.Message = mesaj;
                return Json(mesaj, JsonRequestBehavior.AllowGet);
            }
            mesaj = 2;
            ViewBag.Message = mesaj;
            return Json(mesaj, JsonRequestBehavior.AllowGet);
        }

        // GET: hst_hastadurum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_hastadurum hst_hastadurum = db.hst_hastadurum.Find(id);
            if (hst_hastadurum == null)
            {
                return HttpNotFound();
            }
            return View(hst_hastadurum);
        }

        // POST: hst_hastadurum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hst_hastadurum hst_hastadurum = db.hst_hastadurum.Find(id);
            db.hst_hastadurum.Remove(hst_hastadurum);
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
