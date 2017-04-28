using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models
{
    public class GetMoneyModel
    {
        [StringLength(18, ErrorMessage = "Part numbers must be 18 character in length.")]
        public string IBank { get; set; }

        public string Money { get; set; }

        public string PicturePath { get; set; }

        public bool IsValetIBank { get; set; }
    }
}