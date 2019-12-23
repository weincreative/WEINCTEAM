using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEINCDENTAL.Controllers;
using WEINCDENTAL.Models;

namespace WEINCDENTAL.Security
{
    public class CustomAutAttributes : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var memoryCacher = new MemoryCacheManager();
            var controllerInfo = filterContext.ActionDescriptor;
            List<View_GroupYetki> glist = memoryCacher.Get<List<View_GroupYetki>>("groupyetki");
            List<View_GroupYetki> ulist = memoryCacher.Get<List<View_GroupYetki>>("useryetki");

            if (filterContext != null && (glist != null || ulist != null))
            {
                string controllerName = controllerInfo.ControllerDescriptor.ControllerName;
                string actionName = controllerInfo.ActionName;
                bool yetkiVarMi = false;

                if (glist != null)  //grup yetkileri
                {
                    // grup içinde yetki bakıyor.
                    yetkiVarMi= glist.Any(x => x.ControllerName == controllerName && x.MethodName == actionName);
                    if (!yetkiVarMi) //Grup içinde yetki yoksa...
                    {
                        if (ulist != null) 
                        {
                            yetkiVarMi = ulist.Any(x => x.ControllerName == controllerName && x.MethodName == actionName);
                        }
                    }
                }
                if (ulist != null)
                {
                    yetkiVarMi = ulist.Any(x => x.ControllerName == controllerName && x.MethodName == actionName);
                }

                if (!yetkiVarMi)       // Yetki Yoksa
                {
                    //  filterContext.Result=new JsonResult(new {HttpStatusCode.Forbidden});
                    //filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result=new ViewResult{ViewName = "AccessDenied" };
                    filterContext.HttpContext.Response.StatusCode = 403;
                }
            }

        }
    }
}