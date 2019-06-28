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
    [Authorize(Roles = "1,3,4")]
    public class View_VezneController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: View_Vezne
        public ActionResult ViewVezne_Index()
        {
            return View(db.View_Vezne.ToList());
        }

        // GET: View_Vezne/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Vezne view_Vezne = db.View_Vezne.Find(id);
            if (view_Vezne == null)
            {
                return HttpNotFound();
            }
            return View(view_Vezne);
        }

        // GET: View_Vezne/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: View_Vezne/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_tc,t_adi,t_soyadi,t_basvuru,t_basvurutarihi,Doktoradi,Hareketid,t_hizmetkodu,Hizmetadi,t_fiyat,Hizmettarih,t_odemevarmi,t_odenen,t_kalan,Odemetip,Odemetarih")] View_Vezne view_Vezne)
        {
            if (ModelState.IsValid)
            {
                db.View_Vezne.Add(view_Vezne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_Vezne);
        }

        // GET: View_Vezne/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Vezne view_Vezne = db.View_Vezne.Find(id);
            if (view_Vezne == null)
            {
                return HttpNotFound();
            }
            return View(view_Vezne);
        }

        // POST: View_Vezne/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_tc,t_adi,t_soyadi,t_basvuru,t_basvurutarihi,Doktoradi,Hareketid,t_hizmetkodu,Hizmetadi,t_fiyat,Hizmettarih,t_odemevarmi,t_odenen,t_kalan,Odemetip,Odemetarih")] View_Vezne view_Vezne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_Vezne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_Vezne);
        }

        // GET: View_Vezne/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Vezne view_Vezne = db.View_Vezne.Find(id);
            if (view_Vezne == null)
            {
                return HttpNotFound();
            }
            return View(view_Vezne);
        }

        // POST: View_Vezne/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            View_Vezne view_Vezne = db.View_Vezne.Find(id);
            db.View_Vezne.Remove(view_Vezne);
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
