﻿using System;
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
    [Authorize(Roles = "1,2")]
    public class hst_basvuruController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_basvuru
        public ActionResult Index()
        {
            var hst_basvuru = db.hst_basvuru.Include(h => h.hst_bölüm).Include(h => h.hst_hastakarti);
            return View(hst_basvuru.ToList());
        }
        public ActionResult Hastabasvuru_Index(string id)
        {
            
            var hst_basvuru = db.hst_basvuru.Include(h => h.hst_bölüm).Include(h => h.hst_hastakarti).Where(k=>k.t_aktif==true && k.t_tc==id).ToList();
          
            if (id == null ||hst_basvuru == null) //Yeni Başvuru Açsın.
            {
                return RedirectToAction("Create");
            }
            ViewBag.tc = id;
            return View(hst_basvuru.ToList());
        }


        public PartialViewResult HastaBPartialCreate(string id)
        {
            //if (id==null)
            //{
            //    return RedirectToAction("Create","");
            //}
            ViewBag.t_bolumkodu = new SelectList(db.hst_bölüm, "t_id", "t_adi");
            ViewBag.t_tc = id;
            //ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi");
            return PartialView();
        }
        
        
        
        
        
        // GET: hst_basvuru/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_basvuru hst_basvuru = db.hst_basvuru.Find(id);
            if (hst_basvuru == null)
            {
                return HttpNotFound();
            }
            return View(hst_basvuru);
        }

        // GET: hst_basvuru/Create
        public ActionResult HastaBasvuruCreate()
        {
            ViewBag.t_bolumkodu = new SelectList(db.hst_bölüm, "t_id", "t_adi");
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi");
            return View();
        }

        // POST: hst_basvuru/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_basvuru,t_tc,t_basvurutarihi,t_bolumkodu,t_cagriekraniistem,t_basvurudr,t_taburcu,t_createdate,t_createuser,t_aktif")] hst_basvuru hst_basvuru)
        {

            Ortak o=new Ortak();
            int son=o.GetSonIndex();
            hst_basvuru.t_basvuru = (son + 1).ToString();
            hst_basvuru.t_createuser = System.Web.HttpContext.Current.User.Identity.Name;
            hst_basvuru.t_createdate=DateTime.Now;
            hst_basvuru.t_basvurutarihi=DateTime.Now;
            hst_basvuru.t_basvurudr = 0;
            hst_basvuru.t_cagriekraniistem = 0;
            hst_basvuru.t_taburcu = false;
            hst_basvuru.t_aktif = true;
            // mesaj=0 hiç bir uyarı yok.
            // mesaj=1 başarılı.
            // mesaj=2 başarısız.
            int mesaj = 0;
            if (ModelState.IsValid)
            {
                db.hst_basvuru.Add(hst_basvuru);
                db.SaveChanges();
                mesaj = 1;
                ViewBag.Message = mesaj;
                return RedirectToAction("Hastabasvuru_Index",new{@id=hst_basvuru.t_tc});
            }
            mesaj = 2;
            ViewBag.t_bolumkodu = new SelectList(db.hst_bölüm, "t_id", "t_adi", hst_basvuru.t_bolumkodu);
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_basvuru.t_tc);
            ViewBag.Message = mesaj;
            return View(hst_basvuru);
        }

        // GET: hst_basvuru/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_basvuru hst_basvuru = db.hst_basvuru.Find(id);
            if (hst_basvuru == null)
            {
                return HttpNotFound();
            }
            ViewBag.t_bolumkodu = new SelectList(db.hst_bölüm, "t_id", "t_adi", hst_basvuru.t_bolumkodu);
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_basvuru.t_tc);
            return View(hst_basvuru);
        }

        // POST: hst_basvuru/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_basvuru,t_tc,t_basvurutarihi,t_bolumkodu,t_cagriekraniistem,t_basvurudr,t_taburcu,t_createdate,t_createuser,t_aktif")] hst_basvuru hst_basvuru)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hst_basvuru).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.t_bolumkodu = new SelectList(db.hst_bölüm, "t_id", "t_adi", hst_basvuru.t_bolumkodu);
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_basvuru.t_tc);
            return View(hst_basvuru);
        }

        // GET: hst_basvuru/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hst_basvuru hst_basvuru = db.hst_basvuru.Find(id);
            if (hst_basvuru == null)
            {
                return HttpNotFound();
            }
            return View(hst_basvuru);
        }

        // POST: hst_basvuru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hst_basvuru hst_basvuru = db.hst_basvuru.Find(id);
            db.hst_basvuru.Remove(hst_basvuru);
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
