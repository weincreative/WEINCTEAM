using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    [Authorize(Roles = "1,2")]
    public class VezneController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        // GET: Vezne
        public ActionResult SearchBorc(string tc)
        {
            ViewBag.tc = tc;
            return View();
        }
        public PartialViewResult _CreateVezne(int id)
        {
            ViewBag.hhid=id;
            ViewBag.t_odemetipi = new SelectList(db.hst_odemetip, "t_Id", "t_adi");
            YardimciController yc=new YardimciController();
            ViewBag.kalan = yc.GetKalanBorc(id);
            ViewBag.totalodenen = yc.GetTotalOdenen(id);


            return PartialView();
        }
        
        

        [HttpPost]
        public ActionResult Create()
        {


            return RedirectToAction("SearchBorc",new{tc=""});
        }
    }
}