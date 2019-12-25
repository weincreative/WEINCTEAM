using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;
using WEINCDENTAL.Security;

namespace WEINCDENTAL.Controllers
{
 //   [Authorize(Roles = "1,3,4,5")]
   // [CustomAutAttributes]
    public class View_HastalikDurumController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();

        [HttpGet]
        // GET: View_HastalikDurum
        public PartialViewResult OzelDurumList(string id)
        {
            var hlist = db.View_HastalikDurum.Where(k=>k.t_tc==id && k.t_aktif==true).ToList();
            ViewBag.tc = Ortak._hastatc;
           // ViewBag.t_hdurumid = new SelectList(db.hst_hastalik, "t_id", "t_adi", hst_hastadurum.t_hdurumid);
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
