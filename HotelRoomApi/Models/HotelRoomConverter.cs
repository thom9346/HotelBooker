using SharedModels;

namespace HotelRoomApi.Models
{
    public class HotelRoomConverter : IConverter<HotelRoom, HotelRoomDTO>
    {

        public HotelRoom Convert(HotelRoomDTO sharedHotelRoom)
        {
            return new HotelRoom
            {
                Id = sharedHotelRoom.Id,
                Number = sharedHotelRoom.Number,
                Type = sharedHotelRoom.Type,
                BaseCost = sharedHotelRoom.BaseCost,
               
                
        
            };
        }

        public HotelRoomDTO Convert(HotelRoom hiddenHotelRoom)
        {
            return new HotelRoomDTO
            {
                Id = hiddenHotelRoom.Id,
                Number = hiddenHotelRoom.Number,
                Type = hiddenHotelRoom.Type,
                BaseCost = hiddenHotelRoom.BaseCost,
      
            };
        }
    }
}
