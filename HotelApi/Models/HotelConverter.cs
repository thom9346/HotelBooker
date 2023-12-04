using SharedModels;

namespace HotelApi.Models
{
    public class HotelConverter : IConverter<Hotel, HotelDTO>
    {

        public Hotel Convert(HotelDTO sharedHotel)
        {
            return new Hotel
            {
                Id = sharedHotel.Id,
                Name = sharedHotel.Name,
                Location= sharedHotel.Location,
                Rating = sharedHotel.Rating,
                IsAvailable= sharedHotel.IsAvailable,
                
        
            };
        }

        public HotelDTO Convert(Hotel hiddenHotel)
        {
            return new HotelDTO
            {
                Id = hiddenHotel.Id,
                Name = hiddenHotel.Name,
                Location = hiddenHotel.Location,
                Rating= hiddenHotel.Rating,
                IsAvailable= hiddenHotel.IsAvailable,
      
            };
        }
    }
}
