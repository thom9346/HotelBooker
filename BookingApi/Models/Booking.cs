namespace BookingApi.Models
{
    public class Booking
    { 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int HotelRoomId { get; set; }
        public int CustomerId { get; set; }
    }
}
