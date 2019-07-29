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
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
       

        public ActionResult Index()
        {
            IstatistikController ic=new IstatistikController();


            var query = ic.GetTotalKazanc(2019);
            decimal[] data = new decimal[12];
            foreach (var item in query)
            {
                var i = (item.Ay)-1;
                if (i != null) if (item.Total != null) data[(int) i] = (decimal) item.Total;
            }
            ViewBag.totalhizmet = data;
            return View();
        }
        public ActionResult Ayarlar()
        {
            return View();
        }

      
    }
}