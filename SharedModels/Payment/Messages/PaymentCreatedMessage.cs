using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Payment.Messages
{
    public  class PaymentCreatedMessage
    {
        public int CustomerId { get; set; }

        public int amount { get; set; }
    }
}
