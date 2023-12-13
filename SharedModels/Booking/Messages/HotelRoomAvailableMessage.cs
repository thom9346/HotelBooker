using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Booking.Messages
{
    public class HotelRoomAvailableMessage
    {
        public int CustomerId { get; set; }
        public int BookingId { get; set; }
        public int BaseCost { get; set; }
    }
}
