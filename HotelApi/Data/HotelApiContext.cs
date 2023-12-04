using HotelApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Data
{
    public class HotelApiContext : DbContext
    {
        public HotelApiContext(DbContextOptions<HotelApiContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
