using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    
    /// <summary>
    /// This is the main application controller for authenticated users
    /// </summary>
    [Authorize]
    public class AppController : BaseController
    {
      
        public ActionResult Index()
        {
            string username = UserDetails.UserName;
            ViewBag.UserName = username;
            return View();
        }

    }
}
