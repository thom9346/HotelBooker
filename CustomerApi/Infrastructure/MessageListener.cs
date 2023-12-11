using CustomerApi.Data;
using CustomerApi.Models;
using EasyNetQ;
using SharedModels;

namespace CustomerApi.Infrastructure
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
            // A service scope is created to get an instance of the product repository.
            // When the service scope is disposed, the product repository instance will
            // also be disposed.
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var CustomerRepos = services.GetService<IRepository<Customer>>();


                if (IsCustomerValid(message.CustomerId))
                {

                    var replyMessage = new BookingAcceptedMessage
                    {
                        CustomerValidated = true
                    };

                    bus.PubSub.Publish(replyMessage);

                   
                }
                else
                {
                    // Publish an OrderRejectedMessage
                    var replyMessage = new BookingRejectedMessage
                    {
                        Reason = "Customer was not valid"
                    };

                    bus.PubSub.Publish(replyMessage);
                }
            }
        }

        private bool IsCustomerValid(int id)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var customerRepos = services.GetService<IRepository<Customer>>();

                var customer = customerRepos.Get(id);
                if (customer == null)
                {
                    return false;
                }
                else if(customer.Age >= 18 && customer.Age <= 100)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
