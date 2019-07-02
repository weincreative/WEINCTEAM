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
    public class hst_randevuController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: hst_randevu
        public ActionResult Randevu_Index(string tc)
        {
            try
            {
                if (tc == null)
                {
                    ViewBag.t_tc = new SelectList(db.hst_hastakarti.Where(k => k.t_aktif == true).Select(d => new { HstTCkNo = d.t_tc, HstAd = d.t_tc + " " + d.t_adi + " " + d.t_soyadi }), "HstTCkNo", "HstAd");
                }
                else
                {
                    List<hst_hastakarti> list = null;
                    list = db.hst_hastakarti.Where(k => k.t_aktif == true).ToList();
                    List<SelectListItem> itemList = (from i in list
                                                     select new SelectListItem
                                                     {
                                                         Value = i.t_tc + " " + i.t_adi + " " + i.t_soyadi,
                                                         Text = i.t_tc + " " + i.t_adi + " " + i.t_soyadi,
                                                         Selected = i.t_tc == tc
                                                     }).ToList();
                    ViewBag.t_tc = itemList;
                }

                var hst_randevu = db.hst_randevu.Include(h => h.hst_hastakarti);
                return View(hst_randevu.ToList());
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult HastaListele()
        {
            try
            {
                var sayac = 0;
                var list = db.hst_hastakarti.Where(k => k.t_aktif == true).AsEnumerable().Select(e => new
                {
                    Index = sayac,
                    titleDeger = e.t_tc + " " + e.t_adi + " " + e.t_soyadi
                }).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Randevular()
        {
            try
            {
                var list = db.hst_randevu.Where(k => k.t_aktif == true).AsEnumerable().Select(e => new
                {
                    id = e.t_id,
                    title = e.t_tc + " " + e.hst_hastakarti.t_adi + " " + e.hst_hastakarti.t_soyadi,
                    description = e.t_aciklama,
                    start = Convert.ToDateTime(e.t_baslangicsaat),
                    end = Convert.ToDateTime(e.t_bitissaat),
                    allDay = e.t_allday,
                    className = "event, " + e.t_classname,
                    icon = e.t_icon
                }).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult RandevuPasif(int rid)
        {
            var sonuc = 0;
            try
            {
                hst_randevu randevuPasifeAl = db.hst_randevu.Find(rid);
                randevuPasifeAl.t_aktif = false;
                db.Entry(randevuPasifeAl).State = EntityState.Modified;
                db.SaveChanges();
                sonuc = 1;
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(sonuc, JsonRequestBehavior.AllowGet);
            }
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

        //POST: hst_randevu/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(List<hst_randevu> hst_randevu)
        {
            bool durum = false;
            try
            {
                using (db)
                {
                    db.hst_randevu.AddRange(hst_randevu);
                    db.SaveChanges();
                    durum = true;
                }
                return Json(durum, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(durum, JsonRequestBehavior.AllowGet);
            }
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
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "t_id,t_basvuru,t_tc,t_title,t_aciklama,t_baslangicsaat,t_bitissaat,t_classname,t_icon,t_allday,t_createuser,t_createdate,t_basvurudr,t_aktif")] List<hst_randevu> hst_randevu)
        {
            bool durum = false;
            try
            {
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        hst_randevu.ForEach(p => db.Entry(p).State = EntityState.Modified);
                        db.SaveChanges();
                        durum = true;
                    }
                }
                return Json(durum, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(durum, JsonRequestBehavior.AllowGet);
            }
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
