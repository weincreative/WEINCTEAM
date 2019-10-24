using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    [Authorize(Roles = "1,2")]
    public class AyarlarController : Controller
    {
        
        // GET: Ayarlar
        public PartialViewResult AyarlarKullanicilar()
        {
            string methodAd = "/Ayarlar/AKullanici";
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelKullanicilar = db.adm_kullanicilar.ToList();
            return PartialView(vm);
        }
       
        public PartialViewResult AyarlarModulYetki()
        {
            string methodAd = "/Ayarlar/AModulYetki";
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelModulYetki = db.adm_modulyetki.ToList();
            return PartialView(vm);
        }
       
        public PartialViewResult AyarlarKullaniciGrup()
        {
            string methodAd = "/Ayarlar/AKullaniciGrup";
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelKullaniciGrup = db.adm_kullanicigrup.ToList();
            return PartialView(vm);
        }
      
        public PartialViewResult AyarlarHizmet()
        {
            string methodAd = "/Ayarlar/AHizmetler";
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelHizmet = db.hst_hizmet.ToList();
            return PartialView(vm);
        }

    }
}