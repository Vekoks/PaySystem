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

        public void SetStatusBill(Bill bill, string action ,string resoult)
        {
            bill.StatusBill.Add(new StatusBill()
            {
                Id = Guid.NewGuid(),
                Action = action,
                ActionDate = DateTime.Now,
                ActionResoult = resoult,
            });

            _statusRepo.SaveChanges();

        }


    }
}
