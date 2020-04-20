using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class YardimciController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // T.C. Sorgulama Hasta Kartı..
        [HttpPost]
        public ActionResult GetTc(string tc)
        {
            Ortak._hastatc = tc;
            var varmi = db.hst_hastakarti.Count(k => k.t_aktif == true && k.t_tc == tc) > 0 ? true : false; ;

            if (varmi)
            {
                return RedirectToAction("Hastabasvuru_Index", "hst_basvuru", new { @id = tc });
            }
            else
            {
                return RedirectToAction("Create", "hst_hastakarti");
            }

            //            [HttpPost]
            //public ActionResult Save(EmployeeModel objSave)
            //{

            //    ViewBag.Msg = "Details saved successfully.";
            //    return View();
            //}
            //[HttpPost]
            //public ActionResult Draft(EmployeeModel objDraft)
            //{
            //    ViewBag.Msg = "Details saved as draft.";
            //    return View();
            //}

            //    [HttpPost]
            //[CokluButon(Argument = "btnA", Name = "action")]
            //public ActionResult AAction()
            //{
            //    return RedirectToAction("Index");
            //}

            //[HttpPost]
            //[CokluButon(Argument = "btnB", Name = "action")]
            //public ActionResult BAction()
            //{
            //    return RedirectToAction("Index");
            //}



        }



        // hastanın özel durum sayısını getirir...
        public int GetOzelDurumCount(string id)
        {
            var sonuc = 0;
            try
            {
                sonuc = db.View_HastalikDurum.Count(k => k.t_aktif == true && k.t_tc == id);
                return sonuc;
            }
            catch (Exception e)
            {
                return sonuc;
            }


        }

        public int GetYas(string tc)
        {
            int yas;
            var date = DateTime.Now.Year;
            var hst_tarih = db.hst_hastakarti.Where(k => k.t_tc == tc && k.t_aktif == true)
                .Select(k => k.t_dogumtarihi).FirstOrDefault();

            yas = Convert.ToInt16(date) - Convert.ToInt16(hst_tarih.Year);

            return yas;
        }


        public decimal GetTotalOdenen(int id)
        {
            decimal totalOdenen = 0;
            var vezne = db.View_BsvrVezne.Where(k => k.b_id== id && k.VezneAktif == true &&
            k.HastaAktif == true &&
            k.BorcDurum == true &&
            k.BasvuruAktif == true)
                .ToList();

            if (vezne.Count != 0)
            {
                totalOdenen = vezne.Sum(d => d.t_odenen);
            }
            return totalOdenen;
        }

        public decimal GetKalanBorc(int id)
        {
            decimal kalan = 0;
            if (id != null && id != 0)
            {

                decimal tOdenen = GetTotalOdenen(id);
                decimal? tfiyat = GetTopTutar(id);
                decimal tindirim = GetToplamIndirim(id);
                var o = tfiyat - (tOdenen + tindirim);
                if (o != null) kalan = (decimal)o;
            }
            return kalan;
        }
        public decimal GetTopTutar(int id)
        {
            decimal tutar = 0;
            if (id != null && id!=0)
            {
                var sum = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.BasvuruId == id && k.t_yapildi == true &&
                                                         k.BasvuruAktif == true &&
                                                         k.BorcDurum == true)
                    .Sum(d => d.ToplamBorc);

                if (sum != null)
                    tutar = (decimal)sum;
            }
            return tutar;
        }
        public decimal GetToplamIndirim(int id)
        {
            decimal tutar = 0;
            if (id != null && id != 0)
            {
                var sum = db.View_BsvrVezne.Where(k => k.b_id == id &&
                                                       k.VezneAktif == true &&
                                                       k.HastaAktif == true &&
                                                       k.BorcDurum == true &&
                                                       k.BasvuruAktif == true ).Sum(d => d.t_indirim);
                if (sum != null)
                    tutar = (decimal)sum;
            }
            return tutar;
        }
        public decimal GetTotalOdenen(string tc)
        {
            decimal totalOdenen = 0;
            if (tc != null)
            {
                var vezne = db.View_BsvrVezne.Where(k => k.t_tc == tc && k.VezneAktif == true
                                                         && k.BasvuruAktif == true
                                                         && k.BorcDurum == true
                                                         && k.HastaAktif == true ).ToList();

                if (vezne.Count != 0)
                {
                    totalOdenen = vezne.Sum(d => d.t_odenen);
                }
            }

            return totalOdenen;
        }
        public decimal GetKalanBorc(string tc)
        {
            decimal kalan = 0;
            if (tc != null)
            {

                decimal tOdenen = GetTotalOdenen(tc);
                decimal? tfiyat = GetTopTutar(tc);
                decimal tindirim = GetToplamIndirim(tc);
                var o = tfiyat - (tOdenen + tindirim);
                if (o != null) kalan = (decimal)o;
            }
            return kalan;
        }

        public decimal GetToplamIndirim(string tc)
        {
            decimal tutar = 0;
            if (tc != null)
            {
                var sum = db.View_BsvrVezne.Where(k =>k.t_tc == tc &&
                                                   k.VezneAktif == true &&
                                                   k.HastaAktif == true &&
                                                   k.BorcDurum == true &&
                                                   k.BasvuruAktif == true).Sum(d => d.t_indirim);
                if (sum != null)
                    tutar = (decimal)sum;
            }
            return tutar;
        }

        //T.C. ye ait toplam hizmet tutarı
        public decimal GetTopTutar(string tc)
        {
            decimal tutar = 0;
            if (tc != null)
            {
                var sum = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.TC == tc && k.t_yapildi == true &&
                                                   k.BasvuruAktif == true &&
                                                   k.BorcDurum == true)
                                                   .Sum(d=>d.ToplamBorc);
                
                if (sum != null)
                    tutar = (decimal)sum;
            }
            return tutar;
        }

        //hizmethareket id ye göre tc...
        public string HH_GetTC(int? id)
        {
            string tc = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.HizHareketId == id)
                .Select(d => d.TC).FirstOrDefault();

            return tc;
        }

        
        public string VH_GetTC(int bid)
        {
            string tc = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.BasvuruId==bid && k.BasvuruAktif == true)
                .Select(d => d.TC).FirstOrDefault();

            return tc;
        }

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
                var query = db.View_HizHareket
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

                list = query.OrderByDescending(k => k.HSay).Take(hsay).ToList();


                for (int i = 0; i < list.Count; i++)
                {
                    list[i].color = rgbalist[i];
                    list[i].highlight = rgbalist[i];
                }


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

            var sum = db.hst_his_hareket.Where(k => k.t_aktif == true && k.t_yapildi == true && k.t_islemtarihi >= bas && k.t_islemtarihi < bit)
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
                .Count(k => k.t_aktif == true && k.t_yapildi == true && k.t_islemtarihi >= bas && k.t_islemtarihi < bit && k.t_hizmetkodu == id);
            kazanc = (int)sum;

            return kazanc;
        }
        //Doktorların yaptıkları hizmet sayısı
        public int GetTotalHizmet(string drad, DateTime bas, DateTime bit)
        {
            int kazanc = 0;
            var sum = db.View_HizmetDetay
                .Count(k => k.HHareketAktif == true && k.t_yapildi == true && k.Hizmet_Tarih >= bas && k.Hizmet_Tarih < bit && k.DoktorAd == drad);
            kazanc = (int)sum;

            return kazanc;
        }


    }
}