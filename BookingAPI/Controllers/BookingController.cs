using BookingApi.Data;
using BookingApi.Models;
using Microsoft.AspNetCore.Mvc;
using BookingApi.Infrastructure;
using SharedModels.Booking;
using BookingApi.Services;

namespace BookingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> _repository;
        private readonly IConverter<Booking, BookingDTO> _bookingConverter;
        private readonly IBookingService _bookingService;

        private readonly IMessagePublisher _messagePublisher;

        public BookingController(IRepository<Booking> repository, IConverter<Booking, BookingDTO> converter, IMessagePublisher publisher, IBookingService bookingService)
        {
            _repository = repository;
            _bookingConverter = converter;
            _messagePublisher = publisher;
            _bookingService = bookingService;
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

            // Strip time component from start and end dates
            bookingDto.StartDate = bookingDto.StartDate.Date;
            bookingDto.EndDate = bookingDto.EndDate.Date;


            var booking = _bookingConverter.Convert(bookingDto);

            if (!_bookingService.IsBookingValid(bookingDto))
            {
                return BadRequest("Invalid booking dates. Bookings must minimum 1 day long.");
            } 

            if(_bookingService.DoesBookingOverlap(bookingDto))
            {
                return BadRequest("Dates are already occupied");
            }
            else
            {
                booking.Status = BookingDTO.BookingStatus.tentative;

                var newBooking = _repository.Add(booking);

                _messagePublisher.PublishBookingCreatedMessage(_bookingConverter.Convert(newBooking));

                return CreatedAtRoute("GetBooking", new { id = newBooking.BookingId }, _bookingConverter.Convert(newBooking));
            }

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

        [HttpGet("customer/{id}", Name = "GetBookingsByCustomer")]
        public IEnumerable<Booking> GetByCustomer(int customerId)
        {
            List<Booking> BookingsByCustomer = new List<Booking>();

            foreach (var Booking in _repository.GetAll())
            {
                if (Booking.CustomerId == customerId)
                {
                    BookingsByCustomer.Add(Booking);
                }
            }
            Console.WriteLine(BookingsByCustomer.Count + "found this many bookings!");
            return BookingsByCustomer;
        }
    }
}