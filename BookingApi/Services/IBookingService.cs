using SharedModels.Booking;

namespace BookingApi.Services
{
    public interface IBookingService
    {
        
        bool DoesBookingOverlap(BookingDTO booking);
    }
}
