using SharedModels;
using SharedModels.Booking;

namespace PaymentApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishPaymentCreatedMessage(int CustomerId, int amount);
    }
}
