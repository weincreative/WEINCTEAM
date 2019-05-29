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
    public class View_HastalikDurumController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        // GET: View_HastalikDurum
        public PartialViewResult OzelDurumList(string id)
        {
            var hlist = db.View_HastalikDurum.Where(k=>k.t_tc==id && k.t_aktif==true).ToList();
            ViewBag.tc = Ortak._hastatc;

            return PartialView(hlist);
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
