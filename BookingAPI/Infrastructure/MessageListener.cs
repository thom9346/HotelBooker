using BookingApi.Data;
using BookingApi.Models;
using EasyNetQ;
using SharedModels.Booking;
using SharedModels.Booking.Messages;
using SharedModels.Customer.Messages;

namespace BookingApi.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider provider;
        string connectionString;
        IBus bus;

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
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var bookingRepo = services.GetService<IRepository<Booking>>();

                try
                {
                    var booking = bookingRepo.Get(message.BookingId);

                    booking.Status = BookingDTO.BookingStatus.completed;
                    bookingRepo.Update(booking);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Failed to update booking, issuing refund. Error: " + ex.Message);
                    var refundMessage = new RefundCustomerMessage 
                    { 
                        CustomerId = message.CustomerId, Amount = message.BookingCost
                    };
                    bus.PubSub.Publish(refundMessage);
                } 
                
            }
        }

        private void HandleBookingRejected(BookingRejectedMessage message)
        {
            Console.WriteLine("Booking was rejected, because: " + message.Reason);
            using(var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var bookingRepo = services.GetService<IRepository<Booking>>();

                //delete tentaitve booking.
                bookingRepo.Delete(message.BookingId);
            }
        }

    }
}
