using System.Collections.Generic;
using SharedModels;
using SharedModels.HotelRoom;

namespace BookingApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishBookingCreatedMessage(int customerId, int bookingId, int hotelRoomId);

    }

}
