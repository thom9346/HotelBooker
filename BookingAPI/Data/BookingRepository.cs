using Microsoft.EntityFrameworkCore;
using BookingApi.Models;

namespace BookingApi.Data
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly BookingApiContext db;

        public BookingRepository(BookingApiContext context)
        {
            db = context;
        }
        public Booking Add(Booking entity)
        {
            var newCustomer = db.Bookings.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        public Booking Get(int id)
        {
            return db.Bookings.FirstOrDefault(p => p.BookingId == id);
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings.ToList();
        }

        public void Update(Booking entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var booking = db.Bookings.FirstOrDefault(p => p.BookingId == id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
        }
    }
}
