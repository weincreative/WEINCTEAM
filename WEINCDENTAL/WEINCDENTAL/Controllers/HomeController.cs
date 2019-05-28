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
        public ActionResult Randevu()
        {
            return View();
        }
        public ActionResult HisHareket()
        {
            return View();
        }
        //[HttpPost]
        //[MultipleButton(Name = "Action", Argument = "Sor")]
        //public ActionResult Sor(string tc)
        //{
        //    Ortak._hastatc = tc;
        //    var varmi = db.hst_hastakarti.Count(k => k.t_aktif == true && k.t_tc == tc) > 0 ? true : false; ;

        //    if (varmi)
        //    {
        //        return RedirectToAction("Hastabasvuru_Index", "hst_basvuru", new { @id = tc });
        //    }
        //    else
        //    {
        //        return RedirectToAction("Create", "hst_hastakarti");
        //    }
        //}
        //[HttpPost]
        //[MultipleButton(Name = "Action", Argument = "Hiz")]
        //public ActionResult Hiz(string tc)
        //{
        //    Ortak._hastatc = tc;
        //    return RedirectToAction("HisHareket", "Home");
        //}

        //[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        //public class MultipleButtonAttribute : ActionNameSelectorAttribute
        //{
        //    public string Name { get; set; }
        //    public string Argument { get; set; }

        //    public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        //    {
        //        var isValidName = false;
        //        var keyValue = string.Format("{0}:{1}", Name, Argument);
        //        var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

        //        if (value != null)
        //        {
        //            controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
        //            isValidName = true;
        //        }

        //        return isValidName;
        //    }
        //}
    }
}