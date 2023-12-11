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
                Name = sharedHotelRoom.Name,
                Location= sharedHotelRoom.Location,
                Rating = sharedHotelRoom.Rating,
                IsAvailable= sharedHotelRoom.IsAvailable,
                
        
            };
        }

        public HotelRoomDTO Convert(HotelRoom hiddenHotelRoom)
        {
            return new HotelRoomDTO
            {
                Id = hiddenHotelRoom.Id,
                Name = hiddenHotelRoom.Name,
                Location = hiddenHotelRoom.Location,
                Rating= hiddenHotelRoom.Rating,
                IsAvailable= hiddenHotelRoom.IsAvailable,
      
            };
        }
    }
}
