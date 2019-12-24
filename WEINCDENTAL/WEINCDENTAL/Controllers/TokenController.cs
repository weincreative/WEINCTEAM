using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class TokenController:Controller
    {
        private static WEINCDENTALEntities db = new WEINCDENTALEntities();
        public ICache Cache { get; set; }
        public TokenController()
            : this(new MemoryCacheManager())
        {

        }
        public TokenController(ICache cacheProvider)
        {

            this.Cache = cacheProvider;
        }

        // kullanıcının grubuna göre yetkilerini getirir...
        public List<View_GroupYetki> GetGroupYetkis(int usrId)
        {
            string cachkey = "groupyetki";
            List<View_GroupYetki> currencyData = Cache.Get<List<View_GroupYetki>>(cachkey);
            if (currencyData == null)
            {
                currencyData = db.View_GroupYetki.Where(k => k.UserId == usrId).
                    Where(p => p.UserGroupAktif == true && p.YetkiAktif == true && p.UserAktif == true).ToList();
                if (currencyData.Any())
                {
                    Cache.Add(cachkey, currencyData);
                }
            }
            return currencyData;
        }

        // Kullanıcının grubu dışında extradan verilen yetkileri getirir...
        public List<View_UserYetkis> GetUserYetkis(int usrId)
        {
            string cachkey = "useryetki";
            List<View_UserYetkis> currencyData = Cache.Get<List<View_UserYetkis>>(cachkey);
            if (currencyData == null)
            {
                currencyData = db.View_UserYetkis.Where(k => k.UserId == usrId).
                    Where(p => p.Aktif == true).ToList();
                if (currencyData.Any())
                {
                    Cache.Add(cachkey, currencyData);
                }
            }
            return currencyData;
        }
        // GET: Token
        //public  List<View_kullaniciYetki> YetkileriGetir(int usrId, int groupId)
        //{


        //var memoryCacher = new MemoryCacheManager();
        //string cachkey = "yetki";

        //List<View_kullaniciYetki> list;
        //// Memory Cache de veri yoksa
        //// if (!memoryCacher.Contains(cachkey))
        //if (true)
        //{
        //    list = db.View_kullaniciYetki.Where(k => k.kullaniciId == usrId || k.kullaniciId == groupId).
        //        Where(p => p.yetki == true).ToList();
        //    memoryCacher.Add(cachkey,list);
        //}
        //else
        //{
        //  list = memoryCacher.Get<List<View_kullaniciYetki>>(cachkey);
        //}
        //return list;
        // }
    }
}