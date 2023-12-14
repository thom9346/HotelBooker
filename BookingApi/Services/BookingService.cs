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

        public bool DoesBookingOverlap(BookingDTO newBooking)
        {
            var allBooks = _repository.GetAll();
            foreach (var existingBooking in allBooks)
            {
                if (newBooking.HotelRoomId == existingBooking.HotelRoomId && existingBooking.Status != BookingDTO.BookingStatus.cancelled) // and existing booking is not with cancelled
                {
                    if(newBooking.StartDate < existingBooking.EndDate && existingBooking.StartDate < newBooking.EndDate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
