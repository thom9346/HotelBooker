 using System;
using System.Collections.Generic;
using EasyNetQ;
using Newtonsoft.Json;
using SharedModels.Booking.Messages;
using SharedModels.HotelRoom;

namespace BookingApi.Infrastructure
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

        public void PublishBookingCreatedMessage(int customerId, int bookingId, int hotelRoomId)
        {
            var message = new BookingCreatedMessage
            {
                CustomerId = customerId,
                BookingId = bookingId,
                HotelRoomId = hotelRoomId,
            };

            bus.PubSub.Publish(message);
        }
    }
}
