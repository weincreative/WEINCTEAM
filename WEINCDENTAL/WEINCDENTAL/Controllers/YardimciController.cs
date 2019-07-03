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
            var vezne = db.View_BsvrVezne.Where(k => k.b_id== id && k.VezneAktif == true && k.HastaAktif == true && k.BasvuruAktif == true)
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
                var sum = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.BasvuruId == id &&
                                                         k.BasvuruAktif == true &&
                                                         k.t_borcdurum == true)
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
                                                       k.BasvuruAktif == true).Sum(d => d.t_indirim);
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
                                                         && k.BasvuruAktif == true && k.HastaAktif == true).ToList();

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
                var sum = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.TC == tc &&
                                                   k.BasvuruAktif == true &&
                                                   k.t_borcdurum == true)
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
    }
}