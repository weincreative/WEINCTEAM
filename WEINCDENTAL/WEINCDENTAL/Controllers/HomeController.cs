using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    
    public class HomeController : Controller
    {
        
        IstatistikController ic = new IstatistikController();
        [Authorize(Roles = "1,2")]
        public ActionResult Index()
        {
            string methodAd = "/home/index";
            DateTime dt = DateTime.Today;
            var list = ic.GetListHizmet(dt.Year, dt.Month);
            ViewBag.yil = dt.Year;
            ViewBag.ay = dt.ToString("MMMM");
            decimal? totalKazanc = 0;
            decimal? totalNakit = 0;
            decimal? totalKKart = 0;
            if (list != null)
            {
                totalKazanc = list.Sum(t => t.t_totalborc);
            }
            var tnakit = ic.GetListVezne(dt.Year, dt.Month, 1);
            if (tnakit!=null)
            {
                totalNakit = tnakit.Sum(t => t.t_odenen);
            }
            var tkkart= ic.GetListVezne(dt.Year, dt.Month, 2);
            if (tkkart!=null)
            {
                totalKKart =tkkart.Sum(t => t.t_odenen);
            }
            var data = ic.GetHizmet(dt.Year, dt.Month,4);


            var query = ic.GetTotalKazanc(dt.Year);
            decimal[] datakazanc = new decimal[12];
            foreach (var item in query)
            {
                var i = (item.Ay) - 1;
                if (i != null) if (item.Total != null) datakazanc[(int)i] = (decimal)item.Total;
            }
            ViewBag.totalhizmet = datakazanc;

            ViewBag.hizlist = data;
            ModelIstatistik mis = new ModelIstatistik
            {
                TotalGelir = totalKazanc,
                TotalNakit = totalNakit,
                TotalKKart = totalKKart
            };
            return View(mis);
            
        }
        [Authorize(Roles = "1,2")]
        public ActionResult Ayarlar()
        {
            string methodAd = "/home/ayarlar";
            return View();
        }
        [Authorize(Roles = "3,4,5")]
        public ActionResult SekIndex()
        {
            string methodAd = "/home/Sekindex";
            return View();
        }
    }
}