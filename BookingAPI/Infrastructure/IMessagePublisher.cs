using System.Collections.Generic;
using SharedModels;

namespace Order.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishBookingCreatedMessage(int customerId, int hotelRoomId);
    }

}
