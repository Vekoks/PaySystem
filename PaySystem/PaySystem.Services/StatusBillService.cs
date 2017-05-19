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
        private readonly IPaySystemRepository<StatusBill> _statusRepo;

        public StatusBillService(IPaySystemRepository<StatusBill> userRepository)
        {
            this._statusRepo = userRepository;
        }

        public void DeleteStatusOnBill(Bill bill)
        {
            var statusBill = bill.StatusBill.ToList();

            foreach (var status in statusBill)
            {
                _statusRepo.Delete(status);
            }

            _statusRepo.SaveChanges();
        }

        public ICollection<StatusBill> GetAllStatusOnBill(Bill bill)
        {
            return bill.StatusBill;
        }

        public Guid SetStatusBill(Bill bill, string action ,string resoult)
        {
            var newStatus = new StatusBill()
            {
                Id = Guid.NewGuid(),
                Action = action,
                ActionDate = DateTime.Now,
                ActionResoult = resoult,
            };

            bill.StatusBill.Add(newStatus);

            _statusRepo.SaveChanges();

            return newStatus.Id;
        }

        public void UpDateStatus(StatusBill statusBill, string action, string actionResoult)
        {
            statusBill.Action = action;
            statusBill.ActionResoult = actionResoult;

            _statusRepo.SaveChanges();
        }
    }
}
