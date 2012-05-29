using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using App.Infraestructure;

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

        public ActionResult TaskLists()
        {
            //TODO: get items from data repository
            var list = new List<TaskList>();
            for(var i=0;i<10;i++)
            {
                var tasklist = new TaskList{
                    Name="My task list:"+i
                };
                list.Add(tasklist);
            }

            var result = new OperationResult
            {
                success = true,
                data = list
            };
            //Use custom json result that uses Json.Net (a better serializer for json)
            return new JsonNetResult(result);
            
        }

    }
}
