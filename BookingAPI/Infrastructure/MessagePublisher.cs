 using System;
using System.Collections.Generic;
using EasyNetQ;
using Newtonsoft.Json;
using SharedModels.Booking;
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

        public void PublishBookingCreatedMessage(BookingDTO booking)
        {
            var message = new BookingCreatedMessage
            {
                CustomerId = booking.CustomerId,
                BookingId = booking.BookingId,
                HotelRoomId = booking.HotelRoomId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate
            };

            bus.PubSub.Publish(message);
        }
    }
}
