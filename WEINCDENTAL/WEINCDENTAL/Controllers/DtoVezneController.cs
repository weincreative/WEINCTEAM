using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    [Authorize(Roles = "1,2")]
    public class DtoVezneController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        // GET: DtoVezne
        public PartialViewResult _Vezne2(string tc)
        {
            //tc = "12312312312"; 
            DTO_Vezne dtoVezne=new DTO_Vezne();
            var hhareket = db.View_HizmetDetay.Where(k => k.HHareketAktif== true && k.TC==tc && k.Odemedurumu== false &&k.t_borcdurum==true).ToList();
            var vezne = db.View_Vezne.Where(k => k.t_borcdurum == true && k.t_tc == tc && k.t_odemevarmi == true && k.VezneAktif==true).ToList();
            dtoVezne._ViewModelHDetay = hhareket;
            dtoVezne._ViewModelVezne = vezne;
            dtoVezne.TotalOdenen = vezne.Sum(k => k.t_odenen);

            var hfiyat = db.View_HizmetDetay.Where(k => k.HHareketAktif == true && k.TC == tc && k.t_borcdurum == true).
                Select(k=>k.t_fiyat)
                .DefaultIfEmpty()
                .Sum();
            dtoVezne.TotalKalan = hfiyat - vezne.Sum(k=>k.t_odenen);
            dtoVezne.TotalFiyat = hfiyat;
            return PartialView(dtoVezne);
        }
        

    }
}