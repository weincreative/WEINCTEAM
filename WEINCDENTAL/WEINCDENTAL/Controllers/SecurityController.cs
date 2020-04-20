using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class SecurityController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        private WEINCOPTIONSEntities optionsDB = new WEINCOPTIONSEntities();
       
        // GET: Security
        [AllowAnonymous]
        public ActionResult Login()
        {
            Session.Clear();
            Session.RemoveAll();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(adm_kullanicilar kullanici)
        {
            hst_weincoptions control;
            try
            {
              control= optionsDB.hst_weincoptions.FirstOrDefault(x => x.t_serial != null);
            }
            catch (Exception)
            {
                ViewBag.ErrorMsg = "Kullanıcınız Aktif Edilmemiş veya Sistemimize Kayıtlı Değildir // Yönetici İle Görüşünüz [WEINCREATIVE SECURITY]";
                return View();
            }            
            
            var UserINDB = db.adm_kullanicilar.Where(k=>k.t_aktif==true).FirstOrDefault(x => x.t_kodu == kullanici.t_kodu && x.t_sifre == kullanici.t_sifre);
                       
            if (control.t_serial != null || control.t_serial == "WEINCADMIN")
            {
                if (UserINDB != null)
                {
                    FormsAuthentication.SetAuthCookie(kullanici.t_kodu, false);
                    Session["userId"] = UserINDB.t_id;

                    return RedirectToAction("SekIndex", "Home");

                }
                else
                {
                    ViewBag.ErrorMsg = "Geçersiz Kullanıcı Adı veya Şifre";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Kullanıcınız Aktif Edilmemiş veya Sistemimize Kayıtlı Değildir // Yönetici İle Görüşünüz [WEINCREATIVE SECURITY]";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Ortak._hastatc = "";
            new MemoryCacheManager().Remove("groupyetki");
            new MemoryCacheManager().Remove("useryetki");
            new MemoryCacheManager().Remove("menuyetki");
            return RedirectToAction("Login");
        }
    }
}