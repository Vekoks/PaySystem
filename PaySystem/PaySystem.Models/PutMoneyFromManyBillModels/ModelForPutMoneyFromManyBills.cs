using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Models
{
    public class ModelForPutMoneyFromManyBills
    {
        public string Email { get; set; }

        public List<BillModelForPutMoneyFromManyBill> Bills { get; set; }

        public string IBankOnBillSetMoney { get; set; }

        public bool ExistBills { get; set; }
    }
}
