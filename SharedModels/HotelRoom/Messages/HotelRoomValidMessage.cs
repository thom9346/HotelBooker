using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.HotelRoom.Messages
{
    public class HotelRoomValidMessage
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int BaseCost { get; set; }
        public int HotelRoomId { get; set; }
    }
}
