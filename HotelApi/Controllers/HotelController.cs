using HotelApi.Data;
using HotelApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IRepository<Hotel> _repository;

        public HotelController(IRepository<Hotel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Hotel> GetAll()
        {
            var hotelList = new List<Hotel>(); //this should probably be a customer DTO in the future
            foreach (var hotel in _repository.GetAll())

            {
                hotelList.Add(hotel);
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