﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DB
{
    [Table("User")]
    public class User
    {
        public User()
        {
            this.Customers = new HashSet<Customer>();
            this.Contacts = new HashSet<Contact>();
            this.UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        string Password { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}