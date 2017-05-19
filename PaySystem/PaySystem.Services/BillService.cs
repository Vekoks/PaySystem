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
        private readonly IStatusBillService _statusBillService;

        public BillService(IPaySystemRepository<Bill> userRepository, 
                           IPaySystemRepository<ReallyBill> reallyBilRepo, 
                           IPaySystemRepository<User> userBilRepo,
                           IStatusBillService statusBillService)
        {
            this._billRepo = userRepository;
            this._reallyBilRepo = reallyBilRepo;
            this._userBilRepo = userBilRepo;
            this._statusBillService = statusBillService;
        }

        public void CreateBillOnUser(User user, Bill bill)
        {
            var newBill = new Bill()
            {
                Id = bill.Id,
                Balance = bill.Balance,
                BillType = bill.BillType,
                IBank = bill.IBank,
            };

            user.Bills.Add(newBill);
            _statusBillService.SetStatusBill(newBill, "Create", "succes");

            _billRepo.SaveChanges();
        }

        public Bill GetBillOnUser(User user, string iBank)
        {
            return user.Bills.Where(x => x.IBank == iBank).FirstOrDefault();
        }

        public void GetMoneyInYourBill(Bill bill, string money)
        {
            var moneyForPay = decimal.Parse(money);

            if (bill == null)
            {
                return ;
            }

            var idOnNewStatus = _statusBillService.SetStatusBill(bill, "Get money", "Try");
            var statusOnbillForUpDate = bill.StatusBill.Where(x => x.Id == idOnNewStatus).FirstOrDefault();

            if (bill.Balance < moneyForPay)
            {
                _statusBillService.UpDateStatus(statusOnbillForUpDate, "Get money", "no balance");
            }

            bill.Balance -= decimal.Parse(money);

            _billRepo.SaveChanges();

            _statusBillService.UpDateStatus(statusOnbillForUpDate, "Get money", "succes");
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

            var idOnNewStatus = _statusBillService.SetStatusBill(billToSetMoney, "Put", "Try");
            var statusOnbillForUpDate = billToSetMoney.StatusBill.Where(x => x.Id == idOnNewStatus).FirstOrDefault();

            if (billGetMoneyFromReallyBill == null)
            {
                _statusBillService.UpDateStatus(statusOnbillForUpDate, "Put", "no exist really bill");
                return "no exist really bill";
            }

            else if (billGetMoneyFromReallyBill.Balance < moneyForTransfer)
            {
                _statusBillService.UpDateStatus(statusOnbillForUpDate, "Put", "no balance in really bill");
                return "no balance in really bill";
            }

            else
            {
                billToSetMoney.Balance += moneyForTransfer;

                billGetMoneyFromReallyBill.Balance -= moneyForTransfer;

                _billRepo.SaveChanges();

                _statusBillService.UpDateStatus(statusOnbillForUpDate, "Put", "succes");

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

            var idOnNewStatus = _statusBillService.SetStatusBill(billToSetMoney, "Transferring put money", "Try");
            var statusOnbillForUpDate = billToSetMoney.StatusBill.Where(x => x.Id == idOnNewStatus).FirstOrDefault();

            if (billFromGetMonney.Balance < moneyForTransfer)
            {
                _statusBillService.UpDateStatus(statusOnbillForUpDate, "Transferring put money", "no balance in bill from which I make money");
                return "no balance in bill from which I make money";
            }

            else
            {
                billToSetMoney.Balance += moneyForTransfer;

                billFromGetMonney.Balance -= moneyForTransfer;

                _billRepo.SaveChanges();

                _statusBillService.UpDateStatus(statusOnbillForUpDate, "Transferring put money", "succes");

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

            var idOnNewStatus = _statusBillService.SetStatusBill(billToSetMoney, "Put money from many bills", "Try");
            var statusOnbillForUpDate = billToSetMoney.StatusBill.Where(x => x.Id == idOnNewStatus).FirstOrDefault();

            var billsForGetMoney = model.Bills;

            foreach (var bill in billsForGetMoney)
            {
                var billGetMoneyFromReallyBill = _reallyBilRepo.All().Where(x => x.IBank == bill.IBankOnBillFromGetMoney).FirstOrDefault();
                var moneyForTransfer = decimal.Parse(bill.Money);

                if (billGetMoneyFromReallyBill == null)
                {
                    _statusBillService.UpDateStatus(statusOnbillForUpDate, "Put money from many bills", "no exist really bill");
                    return "no exist really bill";
                }

                else if (billGetMoneyFromReallyBill.Balance < moneyForTransfer)
                {
                    _statusBillService.UpDateStatus(statusOnbillForUpDate, "Put money from many bills", "no balance in really bill");
                    return "no balance in really bill";
                }

                else
                {
                    billToSetMoney.Balance += moneyForTransfer;

                    billGetMoneyFromReallyBill.Balance -= moneyForTransfer;
                }
            }

            _billRepo.SaveChanges();

            _statusBillService.UpDateStatus(statusOnbillForUpDate, "Put money from many bills", "succes");

            return "succes";
        }
    }
}
