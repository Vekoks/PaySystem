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

        public string GetMoneyInYourBill(Bill bill, string money)
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

        public string PutMoneyInYourBill(Bill billToSetMoney, PutMoneyInBillModel model)
        {
            var billGetMoney = _reallyBilRepo.All().Where(x => x.IBank == model.IBankOnBillFromGetMoney).FirstOrDefault();

            var moneyForTransfer = decimal.Parse(model.Money);

            if (billToSetMoney == null)
            {
                return "no exist bill";
            }

            else if (billGetMoney == null)
            {
                return "no exist really bill";
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

        public void DeleteBill(Bill bill)
        {
            _billRepo.Delete(bill);
            _billRepo.SaveChanges();
        }
    }
}
