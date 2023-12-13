using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Booking.Messages
{
    public class BookingAcceptedMessage
    {
        public int CustomerId { get; set; }
        public int BookingId { get; set; }
    }
}
