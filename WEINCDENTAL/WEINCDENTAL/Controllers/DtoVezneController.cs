using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class DtoVezneController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        // GET: DtoVezne
        public ActionResult Index()
        {
            
            var hhareket = db.View_HizmetDetay.Where(k => k.HHareketAktif== true && k.Odemedurumu== false).ToList();
            var vezne = db.View_Vezne.Where(k => k.t_borcdurum == false && k.t_odemevarmi == true).ToList();
            
            return View();
        }
    }
}