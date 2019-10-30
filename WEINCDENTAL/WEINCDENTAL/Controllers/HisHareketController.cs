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
    public class HisHareketController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();


        
        public ActionResult HisHareket(int id)
        {
            string methodAd = "/hizhareket/hizhareket";
            try
            {
                var tc = Ortak._hastatc;
                ViewBag.tc = tc;
                ViewBag.bid = id;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public PartialViewResult DisModul(int id)
        {
            try
            {
                ViewModelHisHareket vm = new ViewModelHisHareket();
                vm._ViewModelHisHareket = db.hst_his_hareket.ToList();
                var hastaYas = Ortak._hastayas;
                ViewBag.hYas = hastaYas;
                ViewBag.basid = id;
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }
        [HttpGet]
       public PartialViewResult _NewHareket()
        {
            try
            {
                //    var hst_firma = db.hst_firma.Include(h => h.hst_marka).ToList();
                ViewBag.t_fid = new SelectList(db.hst_firma.Where(k => k.t_aktif == true), "t_id", "t_fad");
                ViewBag.t_mid = new SelectList(db.hst_marka.Where(k => k.t_aktif == true), "t_id", "t_mad");
                return PartialView();
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }


        public PartialViewResult HisHareketFirma()
        {
            string methodAd = "/hizhareket/Hishareketfirma";
            try
            {
                WEINCDENTALEntities db = new WEINCDENTALEntities();
                ViewModelHisHareket vm = new ViewModelHisHareket();
                vm._ViewModelFirma = db.hst_firma.ToList();
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }
        public PartialViewResult HisHareketPacs()
        {
         
            try
            {
                WEINCDENTALEntities db = new WEINCDENTALEntities();
                ViewModelHisHareket vm = new ViewModelHisHareket();
                vm._ViewModelPacs = db.adm_pacs.ToList();
                return PartialView(vm);
            }
            catch (Exception ex)
            {
                return PartialView();
            }
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