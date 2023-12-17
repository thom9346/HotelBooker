using SharedModels.HotelRoom;

namespace HotelRoomApi.Services
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly Dictionary<HotelRoomType, int> baseCostMapping;
        public HotelRoomService()
        {
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

        public int GetBaseCost(HotelRoomType type)
        {
            if (baseCostMapping.TryGetValue(type, out int cost))
            {
                return cost;
            }
            else
            {
                //TODO: Handle exception where the type is not in the mapping
                //for now, just returning a default value.
                return -1;
            }
        }
    }
}
