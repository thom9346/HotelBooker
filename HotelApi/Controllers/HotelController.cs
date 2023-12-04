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
        public IEnumerable<Hotel> GetAll()
        {
            var hotelList = new List<Hotel>(); 
            foreach (var hotel in _repository.GetAll())
            {
                var hotelDto = _hotelConverter.Convert(hotel);
                hotelList.Add(hotelDto);
            }
            return hotelList;
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
            return new ObjectResult(item);
        }

        // POST hotels
        [HttpPost]
        public IActionResult Post([FromBody] Hotel hotel)
        {
            if (hotel == null)
            {
                return BadRequest();
            }

            var newCustomer = _repository.Add(hotel);

            return CreatedAtRoute("GetHotel", new { id = newCustomer.Id }, newCustomer);
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