using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    [Authorize(Roles = "1,3,4,5")]
    public class View_HizHareketController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: View_HizHareket
        public ActionResult Index()
        {
            return View(db.View_HizHareket.ToList());
        }

        public PartialViewResult PartialHizHareket(int id)
        {
            var hizhareket = db.View_HizHareket.Where(k => k.basvuruid == id).ToList();
            return PartialView(hizhareket);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
