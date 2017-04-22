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
        private readonly IPaySystemRepository<Bill> _billRepo;
        private readonly IPaySystemRepository<ReallyBill> _reallyBilRepo;

        public BillService(IPaySystemRepository<Bill> userRepository, IPaySystemRepository<ReallyBill> reallyBilRepo)
        {
            this._billRepo = userRepository;
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

            _billRepo.SaveChanges();
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

                _billRepo.SaveChanges();

                return "succes";
            }

            return "";
        }

        public string PutMoneyInYourBill(User user, PutMoneyInBillModel model)
        {
            var billToSetMoney = this.GetBillOnUser(user, model.IBankOnBillToSetMoney);

            var billGetMoney = _reallyBilRepo.All().Where(x => x.IBank == model.IBankOnBillFromGetMoney).FirstOrDefault();

            var moneyForTransfer = decimal.Parse(model.Money);

            if (billToSetMoney == null)
            {
                user.Bills.Add(new Bill()
                {
                    Id = Guid.NewGuid(),
                    Balance = moneyForTransfer
                });

                return "no exist bill";
            }
            else if (billGetMoney.Balance < moneyForTransfer)
            {
                return "no balance in really bill";
            }
            else
            {
                billToSetMoney.Balance += moneyForTransfer;

                billGetMoney.Balance -= moneyForTransfer;

                _billRepo.SaveChanges();

                return "succes";
            }

        }

        public ICollection<Bill> GetAllBillsOnUser(User user)
        {
            return user.Bills;
        }

        public Bill GetBillWithId(string billId)
        {
            return _billRepo.All().Where(x => x.Id.ToString().Contains(billId)).FirstOrDefault();
        }
    }
}
