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
            var controllerInfo = filterContext.ActionDescriptor as System.Web.Mvc.ReflectedActionDescriptor;
            string controllerName = controllerInfo.ControllerDescriptor.ControllerName;
            string actionName = controllerInfo.MethodInfo.Name;
            var userId = Convert.ToInt32(HttpContext.Current.Session["userId"]);
            if (userId == 0)
            {
                filterContext.Result=new RedirectResult("Security/Logout");
                
            }
            else
            {
                bool yetkiVarMi = new TokenController().GetYetkis(userId, controllerName, actionName);
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