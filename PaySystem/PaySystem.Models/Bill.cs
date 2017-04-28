using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystem.Models
{
    public class Bill
    {
        private ICollection<StatusBill> statusBill;

        public Bill()
        {
            this.statusBill = new HashSet<StatusBill>();
        }

        public Guid Id { get; set; }

        public BillType BillType { get; set; }

        public decimal Balance { get; set; }

        [MaxLength(18)]
        public string IBank { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<StatusBill> StatusBill
        {
            get
            {
                return statusBill;
            }

            set
            {
                statusBill = value;
            }
        }
    }
}
