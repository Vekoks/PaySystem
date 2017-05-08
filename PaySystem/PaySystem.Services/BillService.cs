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
        private readonly IPaySystemRepository<User> _userBilRepo;

        public BillService(IPaySystemRepository<Bill> userRepository, IPaySystemRepository<ReallyBill> reallyBilRepo, IPaySystemRepository<User> userBilRepo)
        {
            this._billRepo = userRepository;
            this._reallyBilRepo = reallyBilRepo;
            this._userBilRepo = userBilRepo;
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

        public string PutMoneyInYourBill(PutMoneyInBillModel model)
        {
            var billToSetMoney = model.billToSetMoney;

            var billGetMoneyFromReallyBill = _reallyBilRepo.All().Where(x => x.IBank == model.IBankOnBillFromGetMoney).FirstOrDefault();

            var moneyForTransfer = decimal.Parse(model.Money);

            if (billToSetMoney == null)
            {
                return "no exist bill";
            }

            else if (billGetMoneyFromReallyBill == null)
            {
                return "no exist really bill";
            }

            else if (billGetMoneyFromReallyBill.Balance < moneyForTransfer)
            {
                return "no balance in really bill";
            }

            else
            {
                billToSetMoney.Balance += moneyForTransfer;

                billGetMoneyFromReallyBill.Balance -= moneyForTransfer;

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
            return _billRepo.All().Where(x => x.Id.ToString() == billId).FirstOrDefault();
        }

        public void DeleteBill(Bill bill)
        {
            _billRepo.Delete(bill);
            _billRepo.SaveChanges();
        }

        public string TransferringFormBillToBill(PutMoneyInBillModel model, User user)
        {
            var billFromGetMonney = user.Bills.Where(x => x.IBank == model.IBankOnBillFromGetMoney).FirstOrDefault();

            var billToSetMoney = model.billToSetMoney;

            var moneyForTransfer = decimal.Parse(model.Money);

            if (billToSetMoney == null || billFromGetMonney == null)
            {
                return "no exist one of bills";
            }

            else if (billFromGetMonney.Balance < moneyForTransfer)
            {
                return "no balance in bill from which I make money";
            }

            else
            {
                billToSetMoney.Balance += moneyForTransfer;

                billFromGetMonney.Balance -= moneyForTransfer;

                _billRepo.SaveChanges();

                return "succes";
            }
        }

        public string PutMoneyInYourBillFromManyBills(ModelForPutMoneyFromManyBills model)
        {
            var user = _userBilRepo.All().Where(x => x.Email == model.Email).FirstOrDefault();

            var billToSetMoney = user.Bills.Where(x => x.IBank == model.IBankOnBillSetMoney).FirstOrDefault();

            if (billToSetMoney == null)
            {
                return "no exist bill";
            }

            var billsForGetMoney = model.Bills;

            foreach (var bill in billsForGetMoney)
            {
                var billGetMoneyFromReallyBill = _reallyBilRepo.All().Where(x => x.IBank == bill.IBankOnBillFromGetMoney).FirstOrDefault();
                var moneyForTransfer = decimal.Parse(bill.Money);

                if (billGetMoneyFromReallyBill == null)
                {
                    return "no exist really bill";
                }

                else if (billGetMoneyFromReallyBill.Balance < moneyForTransfer)
                {
                    return "no balance in really bill";
                }

                else
                {
                    billToSetMoney.Balance += moneyForTransfer;

                    billGetMoneyFromReallyBill.Balance -= moneyForTransfer;
                }
            }

            _billRepo.SaveChanges();

            return "succes";
        }
    }
}
