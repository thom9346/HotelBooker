using EasyNetQ;
using SharedModels.Booking.Messages;
using SharedModels.Booking;
using SharedModels;
using SharedModels.Payment.Messages;

namespace PaymentApi.Infrastructure
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

        public void PublishPaymentCreatedMessage(int customerId, int amount)
        {
            var message = new PaymentCreatedMessage
            {
                CustomerId = customerId,
                amount = amount
            };

            bus.PubSub.Publish(message);
        }
    }
}
