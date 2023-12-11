using System;
using System.Collections.Generic;
using EasyNetQ;
using SharedModels;

namespace Order.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        IBus bus;

        public MessagePublisher(string connectionString)
        {
            bus = RabbitHutch.CreateBus(connectionString);
        }

        public void Dispose()
        {
            bus.Dispose();
        }

        public void PublishBookingCreatedMessage(int customerId, int hotelRoomId)
        {
            var message = new BookingCreatedMessage
            {
                CustomerId = customerId,
                HotelRoomId = hotelRoomId
            };

            bus.PubSub.Publish(message);
        }
    }
}
