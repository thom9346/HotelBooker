﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Customer.Messages
{
    public class RefundCustomerMessage
    {
        public int CustomerId { get; set; }
        public double Amount { get; set; }
    }
}
