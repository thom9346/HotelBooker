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
                new Customer {Name = "Peter", CustomerId = 1, Age = 25, Email = "peter@mail.com", PhoneNr = "32819663",Balance = 6000},
                new Customer {Name = "Hansi", CustomerId = 2, Age = 12, Email= "emilsad@asmdiklo.com", PhoneNr = "123331551", Balance = 17865},
                new Customer {Name = "Pleb", CustomerId = 3, Age = 23, Email= "Dsd@asmdieeeklo.com", PhoneNr = "123134551", Balance = 500}
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}
