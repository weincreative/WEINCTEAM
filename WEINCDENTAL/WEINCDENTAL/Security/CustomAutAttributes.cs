using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Controllers;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Security
{
    public class CustomAutAttributes: FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var memoryCacher = new MemoryCacheManager();
            List<View_kullaniciYetki> yetkiler = memoryCacher.Get<List<View_kullaniciYetki>>("yetki");


            //throw new NotImplementedException();
        }
    }
}