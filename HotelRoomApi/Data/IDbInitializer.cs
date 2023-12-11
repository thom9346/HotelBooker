namespace HotelRoomApi.Data
{
    public interface IDbInitializer
    {
        void Initialize(HotelRoomApiContext context);
    }
}
