﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Models
{
    public class User : IdentityUser
    {
        private ICollection<Bill> bills;

        public User()
        {
            this.bills = new HashSet<Bill>();
        }

        public override string Id { get; set; }

        public override string UserName { get; set; }

        public string FIsrtName { get; set; }

        public string LastName { get; set; }

        public override string Email { get; set; }

        public virtual ICollection<Bill> Bills
        {
            get{return bills;}
            set{bills = value;}
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }
}
