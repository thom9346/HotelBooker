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
            bus.PubSub.Subscribe<CustomerValidMessage>("CustomerValid", HandleCustomerValid);
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

                var booking = bookingRepo.Get(message.BookingId);
              
                booking.Status = BookingDTO.BookingStatus.completed;
                bookingRepo.Update(booking);
                
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
        private void HandleCustomerValid(CustomerValidMessage message)
        {
            //TODO: check that the hotel is available
          

            //if available
            var replyMessage = new HotelRoomAvailableMessage
            {
                CustomerId = message.CustomerId,
                BookingId = message.BookingId,
                BaseCost = message.BaseCost,
            };
            bus.PubSub.Publish(replyMessage);

            //if not available publish a message. Probably BookingRejectedMessage(?)
        }

    }
}
