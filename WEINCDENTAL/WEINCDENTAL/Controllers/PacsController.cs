using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    [Authorize(Roles = "1,3,4,5")]
    public class PacsController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        // GET: Pacs
        public ActionResult Pacs_Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult _PartialPacs(string tc,string ad,string basDate,string bitDate)
        {
           // tc = "12312312300";
            DateTime? sDate = new DateTime();
            DateTime? fDate = new DateTime();
            if (!String.IsNullOrEmpty(basDate))
            {
                sDate = Convert.ToDateTime(basDate);
            }
            else
            {
                sDate = null;
            }
            if (!String.IsNullOrEmpty(bitDate))
            {
                fDate = Convert.ToDateTime(bitDate);
            }
            else
            {
                fDate = null;
            }
            List<View_Pacs> pacses = null;

            if (String.IsNullOrEmpty(ad) && String.IsNullOrEmpty(tc) && sDate == null && fDate == null)
            {
                pacses = db.View_Pacs.Where(k => k.t_tc == tc && k.HastaAktif == true && k.PacsAktif == true).ToList();
               
            }
            else
            {
                pacses = new BllPacs().PacsList(tc, ad, sDate, fDate).ToList();
                if (pacses.Count == 0)
                {
                    ViewBag.alanKontrol = "Kayıt bulunamadı!";
                }

            }

            return PartialView(pacses);
        }

    
        public ActionResult PacsList(string tc)
        {
            var pacsList = db.View_Pacs.Where(k => k.HastaAktif == true && k.PacsAktif == true && k.t_tc == tc).OrderByDescending(k=>k.t_createdate)
                .ToList();
            

            return View(pacsList);
        }
    }
}