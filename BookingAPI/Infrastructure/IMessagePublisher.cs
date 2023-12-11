using System.Collections.Generic;
using SharedModels;

namespace BookingApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishBookingCreatedMessage(int customerId, int hotelRoomId);
    }

}
