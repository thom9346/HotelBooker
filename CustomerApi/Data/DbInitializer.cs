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
                new Customer {Name = "Peter", CustomerId = 1, Age = 25, Email = "peter@mail.com", PhoneNr = "32819663",Balance = 20}

            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
