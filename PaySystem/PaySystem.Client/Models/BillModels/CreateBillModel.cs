using PaySystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models
{
    public class CreateBillModel
    {
        public string EmailOfUser { get; set; }

        public string BillType { get; set; }

        public decimal Balance { get; set; }

        [StringLength(18, ErrorMessage = "Part numbers must be 18 character in length.")]
        public string IBank { get; set; }
    }
}