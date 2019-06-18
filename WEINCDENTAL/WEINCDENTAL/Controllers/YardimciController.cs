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
            var vezne = db.View_Vezne.Where(k => k.t_borcdurum == true && k.Hareketid == id && k.t_odemevarmi == true).ToList();

            if (vezne.Count!=0)
            {
                totalOdenen = vezne.Sum(d => d.t_odenen);
            }
            return totalOdenen;
        }

        public decimal GetKalanBorc(int id)
        {
            decimal kalan = 0;
            decimal tOdenen = GetTotalOdenen(id);
            var hfiyat =
                db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.HizHareketId == id &&
                                               k.t_borcdurum == true).Select(d=>d.t_fiyat).FirstOrDefault();
             kalan = hfiyat - tOdenen;

            return kalan;
        }
    }
}