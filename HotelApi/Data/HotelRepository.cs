using Microsoft.EntityFrameworkCore;
using HotelApi.Models;

namespace HotelApi.Data
{
    public class HotelRepository : IRepository<Hotel>
    {
        private readonly HotelApiContext db;

        public HotelRepository(HotelApiContext context)
        {
            db = context;
        }
        public Hotel Add(Hotel entity)
        {
            var newCustomer = db.Hotels.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        public Hotel Get(int id)
        {
            return db.Hotels.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Hotel> GetAll()
        {
            return db.Hotels.ToList();
        }

        public void Update(Hotel entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = db.Hotels.FirstOrDefault(p => p.Id == id);
            db.Hotels.Remove(customer);
            db.SaveChanges();
        }
    }
}
