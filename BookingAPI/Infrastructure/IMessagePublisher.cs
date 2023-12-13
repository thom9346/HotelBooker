using System.Collections.Generic;
using SharedModels;
using SharedModels.Booking;
using SharedModels.HotelRoom;

namespace BookingApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishBookingCreatedMessage(BookingDTO booking);

    }

}
