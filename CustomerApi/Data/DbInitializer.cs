using CustomerApi.Models;

namespace CustomerApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(CustomerApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            List <Customer> customers = new List<Customer>
            {
                //new Customer { Name = "Hans", BillingAddress = "sted", ShippingAddress ="sted 2",
                //                    Email ="hans@gmail.com", Phone ="12313123", CreditStanding=100 
                //                   },

            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
