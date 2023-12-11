using SharedModels;

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
                StartDate = hiddenBooking.StartDate,
                EndDate = hiddenBooking.EndDate
            };
        }
    }
}
