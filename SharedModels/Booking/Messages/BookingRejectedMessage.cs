using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Booking.Messages
{
    public class BookingRejectedMessage
    {
        public int BookingId { get; set; }
        public string Reason { get; set; }
    }
}
