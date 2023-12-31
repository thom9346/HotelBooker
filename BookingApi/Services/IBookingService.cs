﻿using SharedModels.Booking;

namespace BookingApi.Services
{
    public interface IBookingService
    {
        bool DoesBookingOverlap(BookingDTO newBooking);
        bool IsBookingValid(BookingDTO booking);
    }
}
