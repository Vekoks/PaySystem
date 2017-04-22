using PaySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models.BillModels
{
    public class AllBillsOnUser
    {
        public BillType BillType { get; set; }

        public string Balance { get; set; }

        public string IBank { get; set; }

        public string BillId { get; set; }
    }
}