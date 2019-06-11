using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class HisHareketController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

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

            //List<View_HizHareket> list = new List<View_HizHareket>()
            //{
            //    new View_HizHareket
            //    {
            //        t_hizmetkodu = 2,
            //        BasvuruNo = "1",
            //        CeneDurum = "Tek",
            //        Doktoradi = "asd"
            //    }
            //};
            //ListHizmet asd=new ListHizmet();
            //asd.ListHareket = list;
            //View_HizHareket hh=new View_HizHareket();
            // hh.t_hizmetkodu = 2;
            // hh.BasvuruNo = "1";
            // hh.CeneDurum = "Tek";
            // hh.Doktoradi = "asd";
            return PartialView(/*asd*/);
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