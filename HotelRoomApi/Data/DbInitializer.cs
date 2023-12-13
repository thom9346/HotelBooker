
using HotelRoomApi.Models;
using SharedModels.HotelRoom;

namespace HotelRoomApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(HotelRoomApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.HotelRooms.Any())
            {
                return;   // DB has been seeded
            }

            List<HotelRoom> customers = new List<HotelRoom>
            {
                new HotelRoom { Id = 1, Number = 1, Type = HotelRoomType.Standard, BaseCost = 600} 
            };

            context.HotelRooms.AddRange(customers);
            context.SaveChanges();
        }
    }
}
