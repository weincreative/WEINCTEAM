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
        // GET: Token
        public  List<View_kullaniciYetki> YetkileriGetir(int usrId, int groupId)
        {
            var memoryCacher = new MemoryCacheManager();
            string cachkey = "yetki";

            List<View_kullaniciYetki> list;
            // Memory Cache de veri yoksa
            // if (!memoryCacher.Contains(cachkey))
            if (true)
            {
                list = db.View_kullaniciYetki.Where(k => k.kullaniciId == usrId || k.kullaniciId == groupId).
                    Where(p => p.yetki == true).ToList();
                memoryCacher.Add(cachkey,list);
            }
            else
            {
              list = memoryCacher.Get<List<View_kullaniciYetki>>(cachkey);
            }
            return list;
        }
    }
}