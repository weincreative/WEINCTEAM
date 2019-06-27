using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class HomeController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        DateTime nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        DateTime sonrakiDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(1);

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Ayarlar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SagUst_Istatistik(int control)
        {
            var sayac = 0;
            if (control == 0)
            {
                var bugunGelenHastaSayisi = db.hst_basvuru.Where(c => c.t_basvurudr == 2 && c.t_aktif == true).Where(k => k.t_basvurutarihi >= nowDate && k.t_basvurutarihi < sonrakiDate).AsEnumerable().Select(e => new
                {
                    Index = sayac,
                    hastaSayisi= e.t_id
                }).ToList().Count();
                return Json(bugunGelenHastaSayisi, JsonRequestBehavior.AllowGet);
            }
            else if (control == 1)
            {
                var bugunYapilanIslemSayisi = db.hst_his_hareket.Where(c => c.t_aktif == true).Where(k => k.t_islemtarihi >= nowDate && k.t_islemtarihi < sonrakiDate).AsEnumerable().Select(e => new
                {
                    Index = sayac,
                    islemSayisi = e.t_hizmetkodu
                }).ToList().Count();
                return Json(bugunYapilanIslemSayisi, JsonRequestBehavior.AllowGet);
            }
            else if (control == 2)
            {
                var bugunAlinanOdemeTutari = db.hst_vezne.Where(c => c.t_aktif == true).Where(k => k.t_odemetarih >= nowDate && k.t_odemetarih < sonrakiDate).AsEnumerable().Select(e => new
                {
                    Index = sayac,
                    topOdeme = e.t_odenen
                }).ToList().Sum(p=>p.topOdeme);
                return Json(bugunAlinanOdemeTutari, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}