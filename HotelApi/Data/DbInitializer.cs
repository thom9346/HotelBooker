
using HotelApi.Models;

namespace HotelApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(HotelApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Hotels.Any())
            {
                return;   // DB has been seeded
            }

            List <Hotel> customers = new List<Hotel>
            {
                //new Customer { Name = "Hans", BillingAddress = "sted", ShippingAddress ="sted 2",
                //                    Email ="hans@gmail.com", Phone ="12313123", CreditStanding=100 
                //                   },

            };

            context.Hotels.AddRange(customers);
            context.SaveChanges();
        }
    }
}
