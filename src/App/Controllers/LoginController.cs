using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.ApplicationBlock;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using App.Services.Users;
using App.Infraestructure;
using System.Threading;
using System.Web.Script.Serialization;
using App.Models;

namespace App.Controllers
{
    /// <summary>
    /// controler used for oauth authentication with twitter
    /// </summary>
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Authenticate with twitter
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult TwitterLogOn(string returnUrl)
        {
            UserService userService = new UserService(); ;
            string screenName;
            int userId;
            if (TwitterConsumer.TryFinishSignInWithTwitter(out screenName, out userId))
            {
                UserDetails user = userService.CreateUserIfNew(screenName, AuthenticationProvider.Twitter);
                /* We use custom principals and identities, store the userdetails in cookie
                 * See http://stackoverflow.com/a/10524305/48025 for details */
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string userdata = serializer.Serialize(user);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        user.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        false,
                        userdata
                    );
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
                return RedirectToAction("Index", "App");

            }
            //TODO: Handle denied (or canceled request)
            else
            {
                //Start authentication process with twitter
                return MessagingUtilities.AsActionResult(TwitterConsumer.StartSignInWithTwitter(true));
            }
            

        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Settings()
        {
            return View();
        }


    }
}
