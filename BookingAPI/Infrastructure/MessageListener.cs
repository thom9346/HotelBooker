using BookingApi.Data;
using BookingApi.Models;
using EasyNetQ;
using SharedModels;

namespace BookingApi.Infrastructure
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
            bus.PubSub.Subscribe<BookingAcceptedMessage>("BookingAccepted", HandleBookingAccepted);
            bus.PubSub.Subscribe<BookingRejectedMessage>("BookingRejected", HandleBookingRejected);
            lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandleBookingAccepted(BookingAcceptedMessage message)
        {

        }

        private void HandleBookingRejected(BookingRejectedMessage message)
        {

        }

    }
}
