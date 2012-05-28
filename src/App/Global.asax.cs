using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Data.Entity;
using System.Web.Security;
using System.Threading;
using System.Web.Management;
using System.Data.SqlClient;
using App.Infraestructure;
using App.Development;
using App.Data;
using App.Services;
using System.Web.Script.Serialization;
using App.Models;

namespace VisualSalesServerWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Errors", // Route name
               "Errors", // URL with parameters
               new { controller = "Home", action = "Errors", id = UrlParameter.Optional }
                // Parameter defaults
           );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                // Parameter defaults
            );



        }


        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                UserDetails user = serializer.Deserialize<UserDetails>(authTicket.UserData);
                AppCustomIdentity identity = new AppCustomIdentity(authTicket);
                identity.UserDetails = user;
                AppCustomPrincipal newUser = new AppCustomPrincipal(identity);
                HttpContext.Current.User = newUser;
            }
        }
    
        protected void Application_Start()
        {
            try
            {


                //Read the application mode
                //Remember to set it to Production before release!
                Application["ApplicationMode"] = Enum.Parse(typeof(ApplicationMode), ConfigurationManager.AppSettings["ApplicationMode"]);

                //Recreate database with test data if model changes
                if (ApplicationState.Mode == ApplicationMode.Development)
                {
                    Database.SetInitializer<AppContext>(new AppDbInitializer());
                    //Force recreation of model
                    using (var db = new AppContext())
                    {
                        db.Users.FirstOrDefault();
                    }
                }
                //Inicialización de entidades que siempre debe ocurrir
                var initService = new InitializationService();
                initService.Init();

                AreaRegistration.RegisterAllAreas();

                RegisterGlobalFilters(GlobalFilters.Filters);
                RegisterRoutes(RouteTable.Routes);
            }
            catch (Exception ex)
            {

                //TODO: Handle and log start exceptions
                throw ex;
            }
        }


    }
}