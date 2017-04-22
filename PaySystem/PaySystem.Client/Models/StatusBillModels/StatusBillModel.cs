using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models.StatusBillModels
{
    public class StatusBillModel
    {
        public string Action { get; set; }

        public DateTime ActionDate { get; set; }

        public string ActionResoult { get; set; }
    }
}