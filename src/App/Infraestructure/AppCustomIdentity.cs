using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using App.DataAcess;
using App.Models;
using App.Services.Users;


namespace App.Infraestructure
{
    public class AppCustomIdentity:IIdentity
    {
        private FormsAuthenticationTicket _ticket;
        private UserDetails _userDetails;
        
        public AppCustomIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
        }
        public string Name
        {
            get { return _ticket.Name; }
        }

        public string AuthenticationType
        {
           get
           {
               return "Custom";
           }
           
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public FormsAuthenticationTicket Ticket
        {
            get { return _ticket; }
        }

        public UserDetails UserDetails
        {
            get
            {
                return _userDetails;
            }
            set
            {
                _userDetails = value;
            }
        }
    }
}