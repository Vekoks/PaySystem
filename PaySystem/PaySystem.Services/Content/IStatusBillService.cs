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
        void SetStatusBill(Bill bill, string action ,string actionResoult);

        ICollection<StatusBill> GetAllStatusOnBill(Bill bill);
    }
}
