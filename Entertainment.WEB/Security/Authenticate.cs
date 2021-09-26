using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Entertainment.WEB.Security
{
    public class Authenticate : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (filterContext.HttpContext.Session["user"] == null)
            {
                //FormsAuthentication.SignOut();
                filterContext.HttpContext.Session.Abandon(); // it will clear the session at the end of request
                filterContext.HttpContext.Session.Clear();
                filterContext.HttpContext.Session.RemoveAll();

                filterContext.HttpContext.Response.Redirect(filterContext.HttpContext.Server.MapPath("~/Account/LogIn"), true);
            }
        }
    }
}