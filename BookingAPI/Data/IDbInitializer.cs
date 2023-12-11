namespace BookingApi.Data
{
    public interface IDbInitializer
    {
        void Initialize(BookingApiContext context);
    }
}
