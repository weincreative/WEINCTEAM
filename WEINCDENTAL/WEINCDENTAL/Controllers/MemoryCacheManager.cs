using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using WebGrease;

namespace WEINCDENTAL.Controllers
{
    public class MemoryCacheManager:ICache
    {
        ObjectCache cache;
        //key varmı yokmu diye control ettiğimiz metot
        public bool Contains(string key)
        {
            return cache.Contains(key);
        }
        //cache key'i ile birlikte cache model'i alıp cache'e ekleyen metot
        public void Add<T>(string key, T source)
        {
            //60 dakika boyunca cache'de tutacak
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)};
            cache.Add(key, source, policy);
        }
        //key parametresi alarak cache'de ki data yı return eden metot
        public T Get<T>(string key)
        {
            return (T)cache.Get(key);
        }
        //key parametresine göre mevcut cache'i silen metot
        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }

}