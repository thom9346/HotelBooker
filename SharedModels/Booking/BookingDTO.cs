using SharedModels.HotelRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Booking
{
    public class BookingDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus Status { get; set; }
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int HotelRoomId { get; set; }

        public enum BookingStatus
        {
            tentative,
            cancelled,
            completed
        }

    }
}
