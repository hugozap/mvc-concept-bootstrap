using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace App.Development
{
    public static class ApplicationState
    {
        public static ApplicationMode Mode
        {
            get
            {
                if (HttpContext.Current.Application["ApplicationMode"] != null)
                {
                    return (ApplicationMode)HttpContext.Current.Application["ApplicationMode"];
                }
                return ApplicationMode.Development;
            }
        }
    }
}