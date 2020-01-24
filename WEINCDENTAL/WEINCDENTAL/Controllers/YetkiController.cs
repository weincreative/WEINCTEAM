using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class YetkiController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        // GET: Yetki
        public ActionResult Yetki_Index()
        {

            List<adm_kullanicilar> users = null;
            users = db.adm_kullanicilar.Where(k => k.t_aktif == true && k.t_id != 1).ToList();
            List<SelectListItem> itemList = (from i in users
                                             select new SelectListItem
                                             {
                                                 Value = i.t_id.ToString(),
                                                 Text = i.t_adi
                                             }).ToList();
            itemList.Add(new SelectListItem
            {
                Value = "0",
                Text = "Kullanıcı Seç",
                Selected = true,
                Disabled = true
            });
            ViewBag.t_kullanicis = itemList.OrderBy(k => k.Value);
           
            return View();
        }

        [HttpPost]
        public JsonResult GetYetkis(int userId)
        {

            List<long> yetkis = db.View_GroupYetki.Where(k =>
                    k.UserGroupAktif == true && k.YetkiAktif == true && k.UserAktif == true && k.UserId == userId)
                .Select(k => k.YetkiId)
                .Distinct().ToList();

            List<long> ekyetki = db.adm_UserYetkis.Where(k => k.Aktif == true && k.UserId == userId)
                .Select(k => k.YetkiId).ToList();

            yetkis.AddRange(ekyetki);

            List<adm_Yetki> kalanyetkiler = db.adm_Yetki.Where(k => k.Aktif == true && k.Id != 17 && k.Id != 6).ToList();

            foreach (var yetki in yetkis)
            {
                kalanyetkiler.RemoveAll(k => k.Id == yetki);
            }
            
            List<SelectListItem> itemList = (from i in kalanyetkiler
                select new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.YetkiAdi
                }).ToList();

            return Json(new{ sonuc= itemList, JsonRequestBehavior.AllowGet});
        }
    }
}