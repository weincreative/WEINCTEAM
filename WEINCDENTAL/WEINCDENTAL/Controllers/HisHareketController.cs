using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class HisHareketController:Controller
    {

        public ActionResult HisHareket(int id)
        {
            var tc = Ortak._hastatc;
            ViewBag.tc = tc;
            ViewBag.bid = id;
            return View();
        }

        public PartialViewResult DisModul(int id)
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelHisHareket vm = new ViewModelHisHareket();
            vm._ViewModelHisHareket = db.hst_his_hareket.ToList();
            ViewBag.basid = id;
            return PartialView(vm);
        }

        public PartialViewResult _NewHareket()
        {

            return PartialView();
        }


        public PartialViewResult HisHareketHizmet()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelHisHareket vm = new ViewModelHisHareket();
            vm._ViewModelHizmet = db.hst_hizmet.ToList();
            return PartialView(vm);
        }
        public PartialViewResult HisHareketBasvuru()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelHisHareket vm = new ViewModelHisHareket();
            vm._ViewModelBasvuru = db.hst_basvuru.ToList();
            return PartialView(vm);
        }
        public PartialViewResult HisHareketHastakarti()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelHisHareket vm = new ViewModelHisHareket();
            vm._ViewModelHastakarti = db.hst_hastakarti.ToList();
            return PartialView(vm);
        }
        public PartialViewResult HisHareketHastaDurum()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelHisHareket vm = new ViewModelHisHareket();
            vm._ViewModelHastaDurum = db.hst_hastadurum.ToList();
            return PartialView(vm);
        }
        public PartialViewResult HisHareketFirma()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelHisHareket vm = new ViewModelHisHareket();
            vm._ViewModelFirma = db.hst_firma.ToList();
            return PartialView(vm);
        }
        public PartialViewResult HisHareketPacs()
        {
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelHisHareket vm = new ViewModelHisHareket();
            vm._ViewModelPacs = db.adm_pacs.ToList();
            return PartialView(vm);
        }
        //public PartialViewResult HisHareketView_HastalikDurum()
        //{
        //    WEINCDENTALEntities db = new WEINCDENTALEntities();
        //    ViewModelHisHareket vm = new ViewModelHisHareket();
        //    vm._ViewModelView_HastalikDurum = db.View_HastalikDurum.ToList();
        //    return PartialView(vm);
        //}
    }
}