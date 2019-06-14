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
        public PartialViewResult _Vezne(string tc)
        {
            tc = "12312312312"; 
            DTO_Vezne dtoVezne=new DTO_Vezne();
            var hhareket = db.View_HizmetDetay.Where(k => k.HHareketAktif== true && k.TC==tc && k.Odemedurumu== false &&k.t_borcdurum==true).ToList();
            var vezne = db.View_Vezne.Where(k => k.t_borcdurum == true && k.t_tc == tc && k.t_odemevarmi == true).ToList();
            dtoVezne._ViewModelHDetay = hhareket;
            dtoVezne._ViewModelVezne = vezne;

            return PartialView(dtoVezne);
        }
    }
}