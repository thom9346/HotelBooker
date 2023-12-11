using Microsoft.EntityFrameworkCore;
using HotelRoomApi.Models;

namespace HotelRoomApi.Data
{
    public class HotelRoomRepository : IRepository<HotelRoom>
    {
        private readonly HotelRoomApiContext db;

        public HotelRoomRepository(HotelRoomApiContext context)
        {
            db = context;
        }
        public HotelRoom Add(HotelRoom entity)
        {
            var newCustomer = db.HotelRooms.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        public HotelRoom Get(int id)
        {
            return db.HotelRooms.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<HotelRoom> GetAll()
        {
            return db.HotelRooms.ToList();
        }

        public void Update(HotelRoom entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = db.HotelRooms.FirstOrDefault(p => p.Id == id);
            db.HotelRooms.Remove(customer);
            db.SaveChanges();
        }
    }
}
