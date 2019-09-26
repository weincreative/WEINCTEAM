using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{

    public class IstatistikController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        DateTime nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        DateTime sonrakiDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(1);

        [HttpGet]
        public ActionResult SagUst_Istatistik(int control)
        {
            var sayac = 0;
            if (control == 0)
            {
                var bugunGelenHastaSayisi = db.hst_basvuru.Where(c => c.t_basvurudr == 2 && c.t_aktif == true).Where(k => k.t_basvurutarihi >= nowDate && k.t_basvurutarihi < sonrakiDate).AsEnumerable().Select(e => new
                {
                    Index = sayac,
                    hastaSayisi = e.t_id
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
                }).ToList().Sum(p => p.topOdeme);
                return Json(bugunAlinanOdemeTutari, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        //Belirli Yıl'a ait Aylık toplam hizmet parası.
        public List<TotalKazanc> GetTotalKazanc(int yil)
        {
            List<TotalKazanc> list = new List<TotalKazanc>();

            try
            {
                list = db.hst_his_hareket.Where(k => k.t_aktif == true && k.t_islemtarihi.Year == yil).OrderByDescending(k => k.t_islemtarihi.Year)
                    .ThenBy(d => d.t_islemtarihi.Month).GroupBy(k => new
                    {
                        k.t_islemtarihi.Month,
                        k.t_islemtarihi.Year
                    }).Select(k => new TotalKazanc()
                    {
                        Ay = k.Key.Month,
                        Yils = k.Key.Year,
                        Total = k.Sum(p => p.t_totalborc)
                    }).ToList();
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return list;
        }

        //Belirli Yıl ve ay'a  ait hizmet listesi...
        public List<hst_his_hareket> GetListHizmet(int yil, int ay)
        {
            List<hst_his_hareket> list = new List<hst_his_hareket>();

            try
            {
                list = db.hst_his_hareket.Where(k => k.t_aktif == true && k.t_islemtarihi.Year == yil &&
                                                     k.t_islemtarihi.Month == ay).ToList();
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return list;
        }
        // Belirli ay yil ve odeme tipine gore vezne list...
        public List<hst_vezne> GetListVezne(int yil, int ay, int otip)
        {
            List<hst_vezne> vezne = new List<hst_vezne>();
            try
            {
                vezne = db.hst_vezne.Where(k => k.t_aktif == true && k.t_odemetipi == otip &&
                                                k.t_odemetarih.Year == yil && k.t_odemetarih.Month == ay).ToList();
                return vezne;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            return vezne;
        }

        // Belirli ay ve yil'a ait en çok yapılan belirli sayıda hizmet kayıdı getir...
        public List<TotalHizmet> GetHizmet(int yil, int ay, int hsay)
        {
            List<TotalHizmet> list = new List<TotalHizmet>();
            string[] rgbalist = new string[] { "rgba(53, 53, 255, 0.9)", "rgba(151,187,205,1)", "rgba(169, 3, 41, 0.7)", "rgba(144,171,86,1)" };
            try
            {
             var  query = db.View_HizHareket
                   .Where(k => k.Baktif == true && k.HHareketAkteif == true && k.HastaAktif == true
                  && k.t_islemtarihi.Year == yil && k.t_islemtarihi.Month == ay
                   )
                   .GroupBy(k => new
                   {
                       k.Hizmetadi,
                       k.t_hizmetkodu
                   }).Select(t => new TotalHizmet()
                   {
                       HAd = t.Key.Hizmetadi,
                       HKod = t.Key.t_hizmetkodu,
                       HSay = t.Count()
                   }).ToList();
                foreach (var item in query)
                {
                    for (int i = 0; i < query.Count; i++)
                    {
                        item.color = rgbalist[i];
                        item.highlight = rgbalist[i];
                    }
                   
                }
                list = query.OrderByDescending(k => k.HSay).Take(hsay).ToList();

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            return list;
        }

        // Başlangıç ve Bitiş tarihli yapılan hizmetlerin toplam parası
        public decimal GetTotalKazanc(DateTime bas, DateTime bit)
        {
            decimal kazanc = 0;

            var sum = db.hst_his_hareket.Where(k => k.t_aktif == true && k.t_islemtarihi >= bas && k.t_islemtarihi < bit)
                .Sum(p => p.t_totalborc);
            if (sum != null)
                kazanc = (decimal)sum;

            return kazanc;
        }
        // Başlangıç ve Bitiş tarihli açılan başvuru sayısı
        public int GetTotalHasta(DateTime bas, DateTime bit)
        {
            int kazanc = 0;

            var sum = db.hst_basvuru
                .Count(k => k.t_aktif == true && k.t_basvurutarihi >= bas && k.t_basvurutarihi < bit);
            kazanc = (int)sum;

            return kazanc;
        }
        // Hizmet id sine göre başlangıç ve bitiş tarihli yapılan toplam hizmet
        public int GetTotalHizmet(int id, DateTime bas, DateTime bit)
        {
            int kazanc = 0;
            var sum = db.hst_his_hareket
                .Count(k => k.t_aktif == true && k.t_islemtarihi >= bas && k.t_islemtarihi < bit && k.t_hizmetkodu == id);
            kazanc = (int)sum;

            return kazanc;
        }
        //Doktorların yaptıkları hizmet sayısı
        public int GetTotalHizmet(string drad, DateTime bas, DateTime bit)
        {
            int kazanc = 0;
            var sum = db.View_HizmetDetay
                .Count(k => k.HHareketAktif == true && k.Hizmet_Tarih >= bas && k.Hizmet_Tarih < bit && k.DoktorAd == drad);
            kazanc = (int)sum;

            return kazanc;
        }
    }
}