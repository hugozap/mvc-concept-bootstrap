using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Services.Utils;
using Newtonsoft.Json;

namespace App.Model
{
    public abstract class BaseEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
         [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
        }
    }
}
