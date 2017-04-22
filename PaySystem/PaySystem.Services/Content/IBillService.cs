﻿using PaySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Services.Content
{
    public interface IBillService
    {
        void CreateBillOnUser(User user, Bill bill);

        ICollection<Bill> GetAllBillsOnUser(User user);

        Bill GetBillOnUser(User user, string iBank);

        Bill GetBillWithId(string billId);

        string PutMoneyInYourBill(User user, PutMoneyInBillModel model);

        string GetMoneyInYourBill(User user, string money);

    }
}
