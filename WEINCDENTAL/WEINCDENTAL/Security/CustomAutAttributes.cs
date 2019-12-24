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
          //  var user=filterContext.HttpContext.User as System.Security.Claims.ClaimsPrincipal;

            var memoryCacher = new MemoryCacheManager();
            var controllerInfo = filterContext.ActionDescriptor as System.Web.Mvc.ReflectedActionDescriptor; ;
            List<View_GroupYetki> glist = null;
            List<View_UserYetkis> ulist = null;
            var userId = Convert.ToInt32(HttpContext.Current.Session["userId"]);

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


            if (filterContext != null && (glist != null || ulist != null))
            {
            //    string controllerName = controllerInfo.ControllerDescriptor.ControllerName;
                string controllerName = controllerInfo.ControllerDescriptor.ControllerName;
                string actionName = controllerInfo.MethodInfo.Name;
                bool yetkiVarMi = false;

                if (glist != null)  //grup yetkileri
                {
                    // grup içinde yetki bakıyor.
                    yetkiVarMi = glist.Any(x => x.ControllerName==controllerName &&
                                                x.MethodName==actionName);
                    if (!yetkiVarMi) //Grup içinde yetki yoksa...
                    {
                        if (ulist != null)
                        {
                            yetkiVarMi = ulist.Any(x => x.ControllerName == controllerName &&
                                                        x.MethodName == actionName);
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
                    filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
                    filterContext.HttpContext.Response.StatusCode = 404;
                }
            }

        }
    }
}