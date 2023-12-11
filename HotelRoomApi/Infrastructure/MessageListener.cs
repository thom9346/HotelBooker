using HotelRoomApi.Data;
using HotelRoomApi.Models;
using EasyNetQ;
using SharedModels;

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
            Console.WriteLine("Hotel Room found a message" + message.ToString);
            // A service scope is created to get an instance of the product repository.
            // When the service scope is disposed, the product repository instance will
            // also be disposed.
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var HotelRoomRepos = services.GetService<IRepository<HotelRoom>>();


                if (IsHotelRoomValid(message.HotelRoomId))
                {

                    var replyMessage = new BookingAcceptedMessage
                    {
                        HotelRoomValidated = true
                    };

                    bus.PubSub.Publish(replyMessage);


                }
                else
                {
                    // Publish  
                    var replyMessage = new BookingRejectedMessage
                    {
                        Reason = "Hotel Room was Rejected"
                    };

                    bus.PubSub.Publish(replyMessage);
                }
            }
        }


        private bool IsHotelRoomValid(int id)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var hotelRoomRepos = services.GetService<IRepository<HotelRoom>>();

                var hotelRoom = hotelRoomRepos.Get(id);
                if (hotelRoom == null)
                {
                    Console.WriteLine("Hotel room not found");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
