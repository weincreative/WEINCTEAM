using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class PacsController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        // GET: Pacs
        public ActionResult Pacs_Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult _PartialPacs()
        {
            string tc = "12312312300";
            IQueryable<View_Pacs> list = null;
            list = db.View_Pacs.Where(k => k.HastaAktif == true && k.PacsAktif == true && k.t_tc == tc)
                .AsQueryable();
            IEnumerable<View_Pacs> pacses = list.GroupBy(d => d.t_tc).Select(g => g.FirstOrDefault()).ToList();

            return PartialView(pacses);
        }
        public ActionResult PacsList()
        {
            return View();
        }
    }
}