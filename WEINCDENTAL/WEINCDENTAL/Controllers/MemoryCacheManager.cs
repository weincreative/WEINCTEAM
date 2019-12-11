using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using WebGrease;

namespace WEINCDENTAL.Controllers
{
    public class MemoryCacheManager : ICache
    {

        private ObjectCache Cache { get { return MemoryCache.Default; } }
        ///
        /// Önbelleğe aldığımız veri okumak için gereken metod.
        ///
        ///Veri çekmek için kullanacağımız anahtar
        /// Önbelleğe alınmış veri
        /// key parametresi alarak cache'de ki data yı return eden metot

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        ///
        /// Önbelleğe veri yazmak için kullanacağımız metod
        ///
        ///Daha sonra önbellekten veriyi okumak kullanılacak anahtar.

        ///Önbelleğe yazılacak veri
        /// cache key'i ile birlikte cache model'i alıp cache'e ekleyen metot
        ///Gün cinsinden bellekte veriyi ne kadar tutuyoruz

        public void Add(string key, object data)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) };
            //policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        ///
        /// Belirtilen anahtarda önbelleğe alınmış veri var mı?
        ///
        ///Anahtar değerimiz
        ///key varmı yokmu diye control ettiğimiz metot

        public bool Contains(string key)
        {
            return (Cache[key] != null);
        }

        ///
        /// Önbelleğe alınmış veriyi silmek için kullanılan metod.
        ///key parametresine göre mevcut cache'i silen metot
        ///Anahtar değerimiz
        public void Remove(string key)
        {
            Cache.Remove(key);
        }

    }

}