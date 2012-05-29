using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Model;
using Newtonsoft.Json;

namespace App.Models
{
    /// <summary>
    /// Represents a task item
    /// Sample entity (feel free to remove it)
    /// </summary>
    public class Task:BaseEntity
    {
        //All BaseEntity childs inherit Id and other properties

        public string Name { get; set; }
        public Task():base()
        {

        }
    }
}