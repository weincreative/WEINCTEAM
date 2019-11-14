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
            var UserINDB = db.adm_kullanicilar.FirstOrDefault(x => x.t_kodu == kullanici.t_kodu && x.t_sifre == kullanici.t_sifre);
       

            if (UserINDB!=null)
            {
                FormsAuthentication.SetAuthCookie(kullanici.t_kodu, false);
                var yetki = db.adm_kullanicilar.Where(k => k.t_aktif == true && k.t_kodu == kullanici.t_kodu)
                    .Select(k => k.t_grup).FirstOrDefault();
                //yetki=> "/home/index" "/home/Sekindex" var mı diye bak.Hangisine yetki varsa onu aç.
                if (yetki==1 || yetki==2)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("SekIndex", "Home");
                }
               
            }
            else
            {
                ViewBag.ErrorMsg = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Ortak._hastatc = "";
            return RedirectToAction("Login");
        }
    }
}