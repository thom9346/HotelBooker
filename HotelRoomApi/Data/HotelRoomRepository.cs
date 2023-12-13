using Microsoft.EntityFrameworkCore;
using HotelRoomApi.Models;
using SharedModels.HotelRoom;

namespace HotelRoomApi.Data
{
    public class HotelRoomRepository : IRepository<HotelRoom>
    {
        private readonly HotelRoomApiContext db;
        private readonly Dictionary<HotelRoomType, int> baseCostMapping;

        public HotelRoomRepository(HotelRoomApiContext context)
        {
            db = context;
            baseCostMapping = new Dictionary<HotelRoomType, int>()
            {
                { HotelRoomType.Standard, 600 },
                { HotelRoomType.Double, 800 },
                { HotelRoomType.Family, 1300 },
                { HotelRoomType.Studio, 1600 },
                { HotelRoomType.Deleuxe, 2500 },
                { HotelRoomType.Suite, 3500 }
            };
        }
        public HotelRoom Add(HotelRoom entity)
        {
            if(baseCostMapping.TryGetValue(entity.Type, out int cost))
            {
                entity.BaseCost = cost;
            }
            else
            {
                //TODO: Handle exception where the type is not in the mapping
                //for now, just setting to -1.
                entity.BaseCost = -1;
            }
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
            if(existingHotelRoom != null)
            {
                if(existingHotelRoom.Type != entity.Type)
                {
                    if(baseCostMapping.TryGetValue(entity.Type, out int newCost))
                    {
                        entity.BaseCost = newCost;
                    }
                    else
                    {
                        //TODO: Handle exception where the type is not in the mapping
                        //for now, just setting to -1.
                        entity.BaseCost = -1;
                    }
                }
            }
            db.Entry(existingHotelRoom).CurrentValues.SetValues(entity);
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
