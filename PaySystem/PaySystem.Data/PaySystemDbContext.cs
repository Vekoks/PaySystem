using Microsoft.AspNet.Identity.EntityFramework;
using PaySystem.Data.Content;
using PaySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Data
{
    public class PaySystemDbContext : IdentityDbContext<User>, IPaySystemDbContext
    {
        public PaySystemDbContext()
            : base("PaySystem")
        {

        }

        public virtual IDbSet<Bill> Bills { get; set; }

        public virtual IDbSet<StatusBill> StatusBills { get; set; }

        public static PaySystemDbContext Create()
        {
            return new PaySystemDbContext();
        }
    }
}
