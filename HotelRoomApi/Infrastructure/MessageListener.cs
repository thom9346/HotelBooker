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
                    bus.PubSub.Subscribe<BookingCreatedMessage>("BookingCreated");
                    lock (this)
                    {
                        Monitor.Wait(this);
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
