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
        public List<View_kullaniciYetki> YetkileriGetir(int usrId, int groupId)
        {
            string cachkey = "yetki";
            List<View_kullaniciYetki> currencyData = Cache.Get<List<View_kullaniciYetki>>(cachkey);
            if (currencyData == null)
            {
                currencyData = db.View_kullaniciYetki.Where(k => k.kullaniciId == usrId || k.kullaniciId == groupId).
                    Where(p => p.yetki == true).ToList();
                if (currencyData.Any())
                {
                    Cache.Add(cachkey, currencyData);
                }
            }
            return currencyData;
        }
    }
}