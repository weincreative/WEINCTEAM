using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Controllers
{
    public class TokenController : Controller
    {
        private static WEINCDENTALEntities db = new WEINCDENTALEntities();
        MemoryCacheManager memoryCacher = new MemoryCacheManager();
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
                currencyData = db.View_UserYetkis.Where(k => k.UserId == usrId).Where(k=>k.Aktif==true).ToList();
                if (currencyData.Any())
                {
                    Cache.Add(cachkey, currencyData);
                }
            }
            return currencyData;
        }

        // GET: Token
        public bool GetYetkis(int userId, string controllerName, string actionName)
        {
            List<View_GroupYetki> glist = null;
            List<View_UserYetkis> ulist = null;
            bool yetkiVarMi = false;

            // Memory Cache de veri yoksa
            if (!memoryCacher.Contains("groupyetki"))
            {
                new TokenController().GetGroupYetkis(userId);
            }
            if (!memoryCacher.Contains("useryetki"))
            {
                new TokenController().GetUserYetkis(userId);
            }
            glist = memoryCacher.Get<List<View_GroupYetki>>("groupyetki");
            ulist = memoryCacher.Get<List<View_UserYetkis>>("useryetki");

            if (glist != null || ulist != null)
            {
                if (glist != null)  //grup yetkileri
                {
                    // grup içinde yetki bakıyor.
                    yetkiVarMi = glist.Any(x => x.ControllerName == controllerName &&
                                                x.MethodName == actionName);
                    if (!yetkiVarMi) //Grup içinde yetki yoksa...
                    {
                        if (ulist != null)
                        {
                            yetkiVarMi = ulist.Any(x => x.ControllerName == controllerName &&
                                                         x.MethodName == actionName);

                        }
                    }
                }
            }

            return yetkiVarMi;
        }

        public List<Tuple<string, string>> MenuYetkiList(int userId)
        {
            string cachkey = "menuyetki";

            List<Tuple<string, string>> currentData = new List<Tuple<string, string>>();
            currentData = Cache.Get<List<Tuple<string, string>>>(cachkey);

            if (currentData == null)
            {
                List<Tuple<string, string>> tupleGlist = new List<Tuple<string, string>>();
                List<Tuple<string, string>> tupleUList = new List<Tuple<string, string>>();
                List<View_GroupYetki> glist = null;
                List<View_UserYetkis> ulist = null;
                // Memory Cache de veri yoksa
                if (!memoryCacher.Contains("groupyetki"))
                {
                    new TokenController().GetGroupYetkis(userId);
                }
                if (!memoryCacher.Contains("useryetki"))
                {
                    new TokenController().GetUserYetkis(userId);
                }
                glist = memoryCacher.Get<List<View_GroupYetki>>("groupyetki");
                ulist = memoryCacher.Get<List<View_UserYetkis>>("useryetki");

                if (glist != null)
                {
                    tupleGlist.AddRange(glist.Select(item => new Tuple<string, string>(item.ControllerName, item.MethodName)));
                }
                if (ulist != null)
                {
                    tupleUList.AddRange(ulist.Select(useritem => new Tuple<string, string>(useritem.ControllerName, useritem.MethodName)));
                }
                List<Tuple<string, string>> yetkiList = new List<Tuple<string, string>>();
                yetkiList.AddRange(tupleGlist.Select(item => new Tuple<string, string>(item.Item1, item.Item2)));
                yetkiList.AddRange(tupleUList.Select(item => new Tuple<string, string>(item.Item1, item.Item2)));
                currentData = yetkiList;
                if (currentData.Any())
                {
                    Cache.Add(cachkey, currentData);
                }

            }

            return currentData;

        }
    }
}