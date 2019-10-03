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
    [Authorize(Roles = "1,2,3,4,5")]
    public class CagriEkraniController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: CagriEkrani
        public ActionResult CagriEkraniIndex()
        {
            var hst_basvuru = db.hst_basvuru.Include(h => h.hst_bölüm).Include(h => h.hst_hastakarti);
            return View(hst_basvuru.ToList());
        }

        public ActionResult BolumleriGetir()
        {
            var sayac = 0;
            var list = db.hst_bölüm.Where(k => k.t_aktif == true).AsEnumerable().Select(e => new
            {
                Index = sayac,
                bolumlerID = e.t_id,
                bolumlerADI = e.t_adi
            }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult PartialCagriEkrani(int bsid, int hcid)
        {
            DateTime nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime sonrakiDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(1);

            ViewBag.Cagirildi = 0;
            if (hcid == 2)
            {
                ViewBag.Cagirildi = 2;
                var cagrilcakList = db.hst_basvuru.Where(x => x.t_cagriekraniistem == 1).Where(x => x.t_bolumkodu == bsid).Where(x => x.t_basvurudr == hcid).Where(x => x.t_basvurutarihi >= nowDate && x.t_basvurutarihi <= sonrakiDate);
                return PartialView(cagrilcakList.ToList());

            }
            else if (hcid == 1)
            {
                ViewBag.Cagirildi = 1;
                var cagrilcakList = db.hst_basvuru.Where(x => x.t_cagriekraniistem == 0).Where(x => x.t_bolumkodu == bsid).Where(x => x.t_basvurudr == hcid).Where(x => x.t_basvurutarihi >= nowDate && x.t_basvurutarihi <= sonrakiDate);
                return PartialView(cagrilcakList.ToList());
            }
            else if (hcid == 0)
            {
                ViewBag.Cagirildi = 0;
                var cagrilcakList = db.hst_basvuru.Where(x => x.t_cagriekraniistem == 0).Where(x => x.t_bolumkodu == bsid).Where(x => x.t_basvurudr == hcid).Where(x=>x.t_basvurutarihi >= nowDate && x.t_basvurutarihi <= sonrakiDate);
                return PartialView(cagrilcakList.ToList());
            }
            return PartialView();
        }

        [HttpPost]
        public JsonResult HastaCagir(int bid)
        {
            var sonuc = 0;
            try
            {
                hst_basvuru hastayiCagir = db.hst_basvuru.Find(bid);
                hastayiCagir.t_basvurudr = 1;
                db.Entry(hastayiCagir).State = EntityState.Modified;
                db.SaveChanges();
                sonuc = 1;
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: CagriEkrani/Details/5
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

        // GET: CagriEkrani/Create
        public ActionResult Create()
        {
            ViewBag.t_bolumkodu = new SelectList(db.hst_bölüm, "t_id", "t_adi");
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi");
            return View();
        }

        // POST: CagriEkrani/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "t_id,t_basvuru,t_tc,t_basvurutarihi,t_bolumkodu,t_cagriekraniistem,t_basvurudr,t_taburcu,t_createdate,t_createuser,t_aktif")] hst_basvuru hst_basvuru)
        {
            if (ModelState.IsValid)
            {
                db.hst_basvuru.Add(hst_basvuru);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.t_bolumkodu = new SelectList(db.hst_bölüm, "t_id", "t_adi", hst_basvuru.t_bolumkodu);
            ViewBag.t_tc = new SelectList(db.hst_hastakarti, "t_tc", "t_adi", hst_basvuru.t_tc);
            return View(hst_basvuru);
        }

        // GET: CagriEkrani/Edit/5
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

        // POST: CagriEkrani/Edit/5
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

        // GET: CagriEkrani/Delete/5
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

        // POST: CagriEkrani/Delete/5
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
