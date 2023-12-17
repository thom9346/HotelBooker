using SharedModels.HotelRoom;

namespace HotelRoomApi.Services
{
    public interface IHotelRoomService
    {
        int GetBaseCost(HotelRoomType type);
    }
}
