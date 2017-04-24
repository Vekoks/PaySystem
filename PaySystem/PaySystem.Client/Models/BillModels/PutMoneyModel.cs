﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models
{
    public class PutMoneyModel
    {
        public string Email { get; set; }

        public string Money { get; set; }

        public string IBankOnBillToSetMoney { get; set; }

        public string IBankOnBillFromGetMoney { get; set; }

        public bool ExistBills { get; set; }
    }
}