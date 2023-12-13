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
            _repository = repository;
        }

        public bool AreChosenDatesAvailable(BookingDTO booking)
        {
            var allBooks = _repository.GetAll();
            foreach (var book in allBooks)
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
