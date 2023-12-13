using BookingApi.Data;
using BookingApi.Models;
using SharedModels.Booking;

namespace BookingApi.Services
{
    public class BookingService : IBookingService
    { 
        private readonly IRepository<Booking> _repository;

        public BookingService(IRepository<Booking> repository)
        {
            repository = _repository;
        }

        public bool AreChosenDatesAvailable(BookingDTO booking)
        {
            foreach (Booking book in _repository.GetAll())
            {
                if (booking.HotelRoomId == book.HotelRoomId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
