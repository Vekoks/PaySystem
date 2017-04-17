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

        void PutMoneyInYourBill(User user, string money);

        void GetMoneyInYourBill(User user, string money);

    }
}
