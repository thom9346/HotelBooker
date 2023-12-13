using SharedModels.HotelRoom;

namespace HotelRoomApi.Models
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public int Number { get; set; } 
        public HotelRoomType Type { get; set; }
        public int BaseCost { get; set; }

    }
}
