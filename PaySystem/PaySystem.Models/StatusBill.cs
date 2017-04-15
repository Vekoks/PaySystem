using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Models
{
    public class StatusBill
    {
        public Guid Id { get; set; }

        public string Action { get; set; }

        public DateTime ActionDate { get; set; }

        public string ActionResoult { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
