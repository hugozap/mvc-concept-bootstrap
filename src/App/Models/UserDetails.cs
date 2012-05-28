using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using App.Model;


namespace App.Models
{
    [Serializable]
    public class UserDetails:BaseEntity
    {
        
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
        

        public UserDetails()
        {
            Active = true;
            Deleted = false;
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

    }
}
