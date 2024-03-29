﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.Models
{
    public class User : BaseEntity
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }

    public User()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.Now;
        }
    }

}
