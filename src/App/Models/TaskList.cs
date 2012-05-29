using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using App.Model;

namespace App.Models
{
    public class TaskList:BaseEntity
    {
        
        public string Name { get; set; }

        public TaskList():base()
         {

         }
    }
}