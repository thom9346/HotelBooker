using SharedModels.Booking;

namespace BookingApi.Services
{
    public interface IBookingService
    {
        
        bool AreChosenDatesAvailable(BookingDTO booking);
    }
}
