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
    public class StatusBillService : IStatusBillService
    {
        private readonly IPaySystemRepository<StatusBill> _userRepo;

        public StatusBillService(IPaySystemRepository<StatusBill> userRepository)
        {
            this._userRepo = userRepository;
        }

        public void SetStatusBill(Bill bill, string action ,string resoult)
        {
            bill.StatusBill.Add(new StatusBill()
            {
                Id = Guid.NewGuid(),
                Action = action,
                ActionDate = DateTime.Now,
                ActionResoult = resoult,
            });

            _userRepo.SaveChanges();

        }
    }
}
