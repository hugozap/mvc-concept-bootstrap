using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Models
{
    public class UserDetailsDto
    {
        public Guid? id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
    }
}
