using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models
{
    public class PutMoneyModel
    {
        public string Email { get; set; }

        public string Money { get; set; }

        [StringLength(18, ErrorMessage = "Part numbers must be 18 character in length.")]
        public string IBankOnBillToSetMoney { get; set; }

        [StringLength(18, ErrorMessage = "Part numbers must be 18 character in length.")]
        public string IBankOnBillFromGetMoney { get; set; }

        public bool ExistBills { get; set; }
    }
}