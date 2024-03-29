﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;
using WEINCDENTAL.Security;

namespace WEINCDENTAL.Controllers
{
    //[Authorize(Roles = "1,2")]
    [CustomAutAttributes]
    public class AyarlarController : Controller
    {
        
        // GET: Ayarlar
        public PartialViewResult AyarlarKullanicilar()
        {
            
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelKullanicilar = db.View_Users.Where(k=>k.t_aktif==true && k.t_grup!=1).ToList();
            return PartialView(vm);
        }
       
        public PartialViewResult AyarlarModulYetki()
        {
            
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelModulYetki = db.adm_modulyetki.ToList();
            return PartialView(vm);
        }
       
        public PartialViewResult AyarlarKullaniciGrup()
        {
            
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelKullaniciGrup = db.adm_kullanicigrup.Where(k=>k.t_id!=1).ToList();
            return PartialView(vm);
        }
      
        public PartialViewResult AyarlarHizmet()
        {
            
            WEINCDENTALEntities db = new WEINCDENTALEntities();
            ViewModelAyarlar vm = new ViewModelAyarlar();
            vm._ViewModelHizmet = db.hst_hizmet.ToList();
            return PartialView(vm);
        }

    }
}