using Microsoft.EntityFrameworkCore;
using HotelRoomApi.Models;
using SharedModels.HotelRoom;
using HotelRoomApi.Services;

namespace HotelRoomApi.Data
{
    public class HotelRoomRepository : IRepository<HotelRoom>
    {
        private readonly HotelRoomApiContext db;
        private readonly Dictionary<HotelRoomType, int> baseCostMapping;
        private readonly IHotelRoomService hotelRoomService;

        public HotelRoomRepository(HotelRoomApiContext context, IHotelRoomService service)
        {
            db = context;
            hotelRoomService = service;
        }
        public HotelRoom Add(HotelRoom entity)
        {
            entity.BaseCost = hotelRoomService.GetBaseCost(entity.Type);
            var newHotel = db.HotelRooms.Add(entity).Entity;
            db.SaveChanges();
            return newHotel;
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
            var existingHotelRoom = db.HotelRooms.FirstOrDefault(p => p.Id == entity.Id);
            if(existingHotelRoom != null && existingHotelRoom.Type != entity.Type)
            {
                entity.BaseCost = hotelRoomService.GetBaseCost(entity.Type);
            }
            db.Entry(existingHotelRoom).CurrentValues.SetValues(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var hotelRoom = db.HotelRooms.FirstOrDefault(p => p.Id == id);
            db.HotelRooms.Remove(hotelRoom);
            db.SaveChanges();
        }
    }
}
