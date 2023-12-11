
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
                new Hotel { Id = 1, IsAvailable = true, Name = "The Golden Inn", Location = "Notting Hill 32", Rating = "4/5"
                                   },

            };

            context.Hotels.AddRange(customers);
            context.SaveChanges();
        }
    }
}
