using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace App.Infraestructure
{
    public class AppCustomPrincipal:IPrincipal
    {
        private AppCustomIdentity _identity;
        public AppCustomPrincipal(AppCustomIdentity identity)
        {
            _identity = identity;
        }

        public bool IsInRole(string role)
        {
            //TODO: role validation code here
            return true;
        }

        public string UserName
        {
            get
            {
                if (_identity == null)
                    return null;
                return _identity.UserDetails.UserName;
            }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }
    }
}