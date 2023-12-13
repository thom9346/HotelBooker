using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Customer.Messages
{
    public class CustomerValidMessage
    {
        public int CustomerId { get; set; }
        public int BookingId { get; set; }
        public int BaseCost { get; set; }
    }
}
