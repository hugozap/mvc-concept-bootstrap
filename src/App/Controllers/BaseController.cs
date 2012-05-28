using System.Web.Mvc;
using System;
using System.Web;
using App.Data;
using App.Models;
using App.Infraestructure;



namespace App.Controllers
{
    [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class BaseController : Controller
    {
        AppContext db = new AppContext();

        //Returns the current user details from the current Identity
        public UserDetails UserDetails
        {
            get
            {
                var identity = (AppCustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity;
                return identity.UserDetails;
            }
        }
        /// <summary>
        /// Error handling for controllers
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            // Bail if we can't do anything; app will crash.
            if (filterContext == null)
                return;
            // since we're handling this, log to elmah

            var ex = filterContext.Exception ?? new Exception("No further information exists.");
            LogException(ex);

            filterContext.ExceptionHandled = true;
            string controllername = (string)filterContext.RouteData.Values["Controller"] ?? "";
            string actionName = (string)filterContext.RouteData.Values["Action"] ?? "";
            var data = new HandleErrorInfo(ex, controllername, actionName);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.StatusDescription = ex.Message;
               
            }
            filterContext.Result = View("Error", data);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        public void LogException(Exception ex)
        {
            //TODO: Log exception here

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}