﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaySystem.Client.Models
{
    public class GetMoneyModel
    {
        public string IBank { get; set; }

        public string Money { get; set; }

        public string PicturePath { get; set; }

        public bool IsValetIBank { get; set; }
    }
}