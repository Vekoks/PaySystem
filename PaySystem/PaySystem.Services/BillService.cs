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

        public BillService(IPaySystemRepository<Bill> userRepository)
        {
            this._userRepo = userRepository;
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

        public Bill GetBillOnUser(User user)
        {
            return user.Bills.FirstOrDefault();
        }

        public void PutMoneyInYourBill(User user, string money)
        {
            var bill = user.Bills.Where(x => x.UserId == user.Id).FirstOrDefault();

            if (bill == null)
            {
                user.Bills.Add(new Bill()
                {
                    Id = Guid.NewGuid(),
                    Balance = decimal.Parse(money)
                });
            }

            else
            {
                bill.Balance += decimal.Parse(money);
            }

            _userRepo.SaveChanges();
        }
    }
}
