using HotelApi.Data;
using HotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace HotelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IRepository<Hotel> _repository;
        private readonly IConverter<Hotel, HotelDTO> _hotelConverter;

        public HotelController(IRepository<Hotel> repository, IConverter<Hotel, HotelDTO> converter)
        {
            _repository = repository;
            _hotelConverter = converter;
        }

        [HttpGet]
        public IEnumerable<HotelDTO> GetAll()
        {
            var hotelDtoList = new List<HotelDTO>();

            foreach (var hotel in _repository.GetAll())
            {
                var hotelDto = _hotelConverter.Convert(hotel);
                hotelDtoList.Add(hotelDto);
            }
            return hotelDtoList;
        }

        // GET hotel/5
        [HttpGet("{id}", Name = "GetHotel")]
        public IActionResult Get(int id)
        {
            var item = _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var hotelDto = _hotelConverter.Convert(item);
            return new ObjectResult(hotelDto);
        }

        // POST hotels
        [HttpPost]
        public IActionResult Post([FromBody] HotelDTO hotelDto)
        {
            if (hotelDto == null)
            {
                return BadRequest();
            }

            var hotel = _hotelConverter.Convert(hotelDto);
            var newCustomer = _repository.Add(hotel);

            return CreatedAtRoute("GetHotel", new { id = newCustomer.Id },
                _hotelConverter.Convert(newCustomer));
        }

        [HttpDelete]
        public IActionResult DeleteHotelById([FromBody] int id)
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