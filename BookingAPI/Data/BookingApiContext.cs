using BookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data
{
    public class BookingApiContext : DbContext
    {
        public BookingApiContext(DbContextOptions<BookingApiContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
