using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class UserYetkiController : Controller
    {
        private WEINCDENTALEntities db = new WEINCDENTALEntities();
        [HttpPost]
        public JsonResult YetkisCreate(int userId, List<long> yetkiIdList)
        {
            bool durum = false;
            List<adm_UserYetkis> userYetki = new List<adm_UserYetkis>();
            foreach (var item in yetkiIdList)
            {
                adm_UserYetkis list = new adm_UserYetkis
                {

                    UserId = userId,
                    YetkiId = item,
                    Aktif = true

                };
                userYetki.Add(list);

            }
            // Group İşlemleri yetkisi var, Kullanıcı işlemleri yetkisi yoksa =Kullanıcı işlemleri yetkisini de ekle...
            if (userYetki.Count(k => k.YetkiId == 1 && k.YetkiId != 2) != 0)
            {
                userYetki.Add(new adm_UserYetkis
                {
                    UserId = userId,
                    YetkiId = 2,
                    Aktif = true
                });
            }
            // Kullanıcı işlemleri yetkisi var, Group işlemleri yetkisi yoksa = Group işlemleri yetkisini de ekle...
            else if (userYetki.Count(k => k.YetkiId == 2 && k.YetkiId != 1) != 0)
            {
                userYetki.Add(new adm_UserYetkis
                {
                    UserId = userId,
                    YetkiId = 1,
                    Aktif = true
                });
            }

            try
            {
                using (db)
                {
                    db.adm_UserYetkis.AddRange(userYetki);
                    db.SaveChanges();
                    durum = true;
                }
                return Json(durum, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(durum, JsonRequestBehavior.AllowGet);

            }



        }
    }
}