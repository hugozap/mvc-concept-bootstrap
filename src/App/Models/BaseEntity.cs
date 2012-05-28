using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Services.Utils;

namespace App.Model
{
    public abstract class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
        public string UserCreate { get; set; }
        public string UserModified { get; set; }
        public DateTime? DeletedDate { get; set; }

        protected BaseEntity()
        {
            DateCreated = DateTime.UtcNow;
            DateModified = DateCreated;
        }

        public DateTime DateCreatedUtc
        {
            get{
                return DateUtils.SetUtcDateKind(DateCreated);
            }
            
        }

        public DateTime DateModifiedUtc
        {
            get
            {
                return DateUtils.SetUtcDateKind(DateModified);
            }
        }


    }
}
