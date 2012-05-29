using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Infraestructure
{
    /// <summary>
    /// 
    /// This class contains the result that will be sent to the client.
    /// (javascript naming conventions used to make it easy for the client)
    /// </summary>
    public class OperationResult
    {
        public bool success;
        public string error;
        public object data;
    }
}