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
            return View();
        }
        public ActionResult Ayarlar()
        {
            return View();
        }

      
    }
}