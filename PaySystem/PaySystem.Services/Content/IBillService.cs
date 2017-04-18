using PaySystem.Models;
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

        Bill GetBillOnUser(User user, string iBank);

        string PutMoneyInYourBill(User user, PutMoneyInBillModel model);

        string GetMoneyInYourBill(User user, string money);

    }
}
