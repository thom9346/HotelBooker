using SharedModels.Booking;

namespace BookingApi.Models
{
    public class BookingConverter : IConverter<Booking, BookingDTO>
    {

        public Booking Convert(BookingDTO sharedBooking)
        {
            return new Booking
            {
                BookingId = sharedBooking.BookingId,
                CustomerId = sharedBooking.CustomerId,
                HotelRoomId = sharedBooking.HotelRoomId,
                Status = sharedBooking.Status,
                StartDate = sharedBooking.StartDate,
                EndDate = sharedBooking.EndDate,
            };
        }

        public BookingDTO Convert(Booking hiddenBooking)
        {
            return new BookingDTO
            {
                BookingId = hiddenBooking.BookingId,
                CustomerId = hiddenBooking.CustomerId,
                HotelRoomId = hiddenBooking.HotelRoomId,
                Status = hiddenBooking.Status,
                StartDate = hiddenBooking.StartDate,
                EndDate = hiddenBooking.EndDate
            };
        }
    }
}
