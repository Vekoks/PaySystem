using PaySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models
{
    public class CreateBillModel
    {
        public string EmailOfUser { get; set; }

        public string BillType { get; set; }

        public decimal Balance { get; set; }

        public string IBank { get; set; }
    }
}