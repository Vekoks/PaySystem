using PaySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Services.Content
{
    public interface IStatusBillService
    {
        Guid SetStatusBill(Bill bill, string action ,string actionResoult);

        ICollection<StatusBill> GetAllStatusOnBill(Bill bill);

        void DeleteStatusOnBill(Bill bill);

        void UpDateStatus(StatusBill statusBill, string action, string actionResoult);
    }
}
