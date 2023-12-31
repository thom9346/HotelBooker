﻿using SharedModels.HotelRoom;
using static SharedModels.Booking.BookingDTO;

namespace BookingApi.Models
{
    public class Booking
    { 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus Status { get; set; }
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int HotelRoomId { get; set; }
    }
}
