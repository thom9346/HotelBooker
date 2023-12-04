namespace HotelApi.Data
{
    public interface IDbInitializer
    {
        void Initialize(HotelApiContext context);
    }
}
