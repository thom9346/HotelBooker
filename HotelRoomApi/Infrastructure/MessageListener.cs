using HotelRoomApi.Data;
using HotelRoomApi.Models;
using EasyNetQ;
using SharedModels.Booking.Messages;
using SharedModels.HotelRoom.Messages;
using SharedModels.HotelRoom;

namespace HotelRoomApi.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider provider;
        string connectionString;
        IBus bus;

        // The service provider is passed as a parameter, because the class needs
        // access to the product repository. With the service provider, we can create
        // a service scope that can provide an instance of the product repository.
        public MessageListener(IServiceProvider provider, string connectionString)
        {
            this.provider = provider;
            this.connectionString = connectionString;
        }

        public void Start()
        {
            //for some reason im still getting the "A task was cancelled" error.
            //Introduced a thread.sleep for now, but we should prob just copy/paste the retry policy
            //I didnt do it yet, because I am clueless why its needed right now
            Thread.Sleep(10000);
            using (bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.PubSub.Subscribe<BookingCreatedMessage>("BookingCreated", HandleBookingCreated);
                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandleBookingCreated(BookingCreatedMessage message)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var hotelRoomRepos = services.GetService<IRepository<HotelRoom>>();

                var hotelRoom = hotelRoomRepos.Get(message.HotelRoomId);

                TimeSpan duration = message.EndDate - message.StartDate;
                var bookingCost = hotelRoom.BaseCost * duration.TotalDays;

                if (hotelRoom == null)
                {
                    var rejectionMessage = new BookingRejectedMessage
                    {
                        BookingId = message.BookingId,
                        Reason = $"Hotel Room with ID: {message.HotelRoomId} does not exist"
                    };
                    bus.PubSub.Publish(rejectionMessage);
                }
                else
                {
                    var replyMessage = new HotelRoomValidMessage
                    {
                        BookingId = message.BookingId,
                        CustomerId = message.CustomerId,
                        BookingCost = bookingCost,
                        HotelRoomId = message.HotelRoomId,
                    };

                    bus.PubSub.Publish(replyMessage);
                }
            }
        }
    }
}
