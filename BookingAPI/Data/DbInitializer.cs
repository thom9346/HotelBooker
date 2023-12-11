
using BookingApi.Models;

namespace BookingApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(BookingApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Bookings.Any())
            {
                return;   // DB has been seeded
            }

            List<Booking> customers = new List<Booking>
            {
                new Booking { BookingId = 1, CustomerId = 1, EndDate = new DateTime(), StartDate = new DateTime()} 
            };

            context.Bookings.AddRange(customers);
            context.SaveChanges();
        }
    }
}
