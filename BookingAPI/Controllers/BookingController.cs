using BookingApi.Data;
using BookingApi.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace BookingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> _repository;
        private readonly IConverter<Booking, BookingDTO> _bookingConverter;

        public BookingController(IRepository<Booking> repository, IConverter<Booking, BookingDTO> converter)
        {
            _repository = repository;
            _bookingConverter = converter;
        }

        [HttpGet]
        public IEnumerable<BookingDTO> GetAll()
        {
            var bookingDtoList = new List<BookingDTO>();

            foreach (var booking in _repository.GetAll())
            {
                var bookingDto = _bookingConverter.Convert(booking);
                bookingDtoList.Add(bookingDto);
            }
            return bookingDtoList;
        }

        // GET booking/5
        [HttpGet("{id}", Name = "GetBooking")]
        public IActionResult Get(int id)
        {
            var item = _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var bookingDto = _bookingConverter.Convert(item);
            return new ObjectResult(bookingDto);
        }

        // POST bookings
        [HttpPost]
        public IActionResult Post([FromBody] BookingDTO bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest();
            }

            var booking = _bookingConverter.Convert(bookingDto);
            var newBooking = _repository.Add(booking);

            return CreatedAtRoute("GetBooking", new { id = newBooking.BookingId },
                _bookingConverter.Convert(newBooking));
        }

        [HttpDelete]
        public IActionResult DeleteBookingById([FromBody] int id)
        {
            if (_repository.Get(id) == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return Ok();
        }
    }
}