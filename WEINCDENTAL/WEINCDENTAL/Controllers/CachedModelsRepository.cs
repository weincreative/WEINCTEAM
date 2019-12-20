using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class CachedModelsRepository
    {
        private static WEINCDENTALEntities db = new WEINCDENTALEntities();
        public ICache Cache { get; set; }
        public CachedModelsRepository()
            : this(new MemoryCacheManager())
        {

        }
        public CachedModelsRepository(ICache cacheProvider)
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
                    Where(p => p.UserGroupAktif == true && p.YetkiAktif==true && p.UserAktif==true).ToList();
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
    }
}