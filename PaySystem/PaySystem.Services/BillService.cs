using PaySystem.Services.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaySystem.Models;
using PaySystem.Data.Repository;

namespace PaySystem.Services
{
    public class BillService : IBillService
    {
        private readonly IPaySystemRepository<Bill> _userRepo;
        private readonly IPaySystemRepository<ReallyBill> _reallyBilRepo;

        public BillService(IPaySystemRepository<Bill> userRepository, IPaySystemRepository<ReallyBill> reallyBilRepo)
        {
            this._userRepo = userRepository;
            this._reallyBilRepo = reallyBilRepo;
        }

        public void CreateBillOnUser(User user, Bill bill)
        {
            user.Bills.Add(new Bill()
            {
                Id = bill.Id,
                Balance = bill.Balance,
                BillType = bill.BillType,
                IBank = bill.IBank,
            });

            _userRepo.SaveChanges();
        }

        public Bill GetBillOnUser(User user, string iBank)
        {
            return user.Bills.Where(x => x.IBank == iBank).FirstOrDefault();
        }

        public string GetMoneyInYourBill(User user, string money)
        {
            var bill = user.Bills.Where(x => x.UserId == user.Id).FirstOrDefault();

            if (bill != null)
            {
                var moneyForPay = decimal.Parse(money);

                if (bill.Balance < moneyForPay)
                {
                    return "no balance";
                }

                bill.Balance -= decimal.Parse(money);

                _userRepo.SaveChanges();

                return "succes";
            }

            return "";
        }

        public string PutMoneyInYourBill(User user, PutMoneyInBillModel model)
        {
            var billToSetMoney = user.Bills.Where(x => x.UserId == user.Id).Where(b => b.IBank == model.IBankOnBillToSetMoney).FirstOrDefault();

            var billGetMoney = _reallyBilRepo.All().Where(x => x.IBank == model.IBankOnBillFromGetMoney).FirstOrDefault();

            var moneyForTransle = decimal.Parse(model.Money);

            if (billToSetMoney == null)
            {
                user.Bills.Add(new Bill()
                {
                    Id = Guid.NewGuid(),
                    Balance = moneyForTransle
                });

                return "no exist bill";
            }
            else if (billGetMoney.Balance < moneyForTransle)
            {
                return "no balance in really bill";
            }
            else
            {
                billToSetMoney.Balance += moneyForTransle;

                billGetMoney.Balance -= moneyForTransle;

                _userRepo.SaveChanges();

                return "succes";
            }

        }
    }
}
