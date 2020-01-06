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
    public class View_GroupYetkiController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: View_GroupYetki
        public ActionResult Index()
        {
            return View(db.View_GroupYetki.ToList());
        }

        // GET: View_GroupYetki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_GroupYetki view_GroupYetki = db.View_GroupYetki.Find(id);
            if (view_GroupYetki == null)
            {
                return HttpNotFound();
            }
            return View(view_GroupYetki);
        }

        // GET: View_GroupYetki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: View_GroupYetki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,t_kid,t_kodu,YM_Id,YetkiId,MethodId,GroupId,groupAd,YetkiAdi,YetkiAciklama,GorunecekIsim,MethodName,ControllerName,UserGroupAktif,YetkiAktif,UserAktif")] View_GroupYetki view_GroupYetki)
        {
            if (ModelState.IsValid)
            {
                db.View_GroupYetki.Add(view_GroupYetki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_GroupYetki);
        }

        // GET: View_GroupYetki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_GroupYetki view_GroupYetki = db.View_GroupYetki.Find(id);
            if (view_GroupYetki == null)
            {
                return HttpNotFound();
            }
            return View(view_GroupYetki);
        }

        // POST: View_GroupYetki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,t_kid,t_kodu,YM_Id,YetkiId,MethodId,GroupId,groupAd,YetkiAdi,YetkiAciklama,GorunecekIsim,MethodName,ControllerName,UserGroupAktif,YetkiAktif,UserAktif")] View_GroupYetki view_GroupYetki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_GroupYetki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_GroupYetki);
        }

        // GET: View_GroupYetki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_GroupYetki view_GroupYetki = db.View_GroupYetki.Find(id);
            if (view_GroupYetki == null)
            {
                return HttpNotFound();
            }
            return View(view_GroupYetki);
        }

        // POST: View_GroupYetki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            View_GroupYetki view_GroupYetki = db.View_GroupYetki.Find(id);
            db.View_GroupYetki.Remove(view_GroupYetki);
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
