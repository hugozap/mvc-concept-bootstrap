using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

using System.Data.Entity;
using App.Models;
using App.Data;

namespace App.Services.Users
{
    public enum AuthenticationProvider{Twitter,Facebook};
    public class UserService
    {
       

        public UserDetails GetUserDetails(string userName)
        {
            using (var db = new AppContext())
            {
                var details = db.Users.SingleOrDefault(u => u.UserName == userName);
                return details;

            }
        }

       
        /// <summary>
        /// Creates the user in the database. avoid conflicts we append the name of the provider
        /// Ex: Twitter-UserName
        /// </summary>
        /// <param name="screenName">screen name returned by provider</param>
        /// <param name="authprovider"></param>
        public UserDetails CreateUserIfNew(string screenName, AuthenticationProvider authprovider)
        {
            string username = authprovider.ToString() + "-" + screenName;
            using (var db = new AppContext())
            {
                var details = db.Users.Where(u => u.UserName == username).FirstOrDefault();
                if (details == null)
                {
                    details = new UserDetails()
                    {
                        Id = Guid.NewGuid(),
                        UserName = username                        
                    };
                    db.Users.Add(details);
                    db.SaveChanges();
                    
                }
                return details;
            }
            
        }

        public void UpdateUser(UserDetails userdetails)
        {
            if (userdetails.Id == Guid.Empty)
                throw new ApplicationException("User Id empty");

            using (var db = new AppContext())
            {
                var user = (from usr in db.Users where usr.Id == userdetails.Id select usr).FirstOrDefault();
                var entry = db.Entry<UserDetails>(user);
                entry.CurrentValues.SetValues(userdetails);
                db.SaveChanges();
            }

        }



    


        public bool ValidateCredentials(string user, string pass)
        {
            return Membership.ValidateUser(user, pass);
        }
    }
       
}
