using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Models
{
    public class BillModelForPutMoneyFromManyBill
    {
        public string Money { get; set; }

        [StringLength(18, ErrorMessage = "Part numbers must be 18 character in length.")]
        public string IBankOnBillFromGetMoney { get; set; }
    }
}
