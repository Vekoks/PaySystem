using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Models
{
    public class ReallyBill
    {
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public string IBank { get; set; }
    }
}
