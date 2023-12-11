using HotelRoomApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomApi.Data
{
    public class HotelRoomApiContext : DbContext
    {
        public HotelRoomApiContext(DbContextOptions<HotelRoomApiContext> options)
            : base(options)
        {
        }

        public DbSet<HotelRoom> HotelRooms { get; set; }
    }
}
