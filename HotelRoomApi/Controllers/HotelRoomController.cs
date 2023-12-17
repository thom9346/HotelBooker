using HotelRoomApi.Data;
using HotelRoomApi.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModels.HotelRoom;

namespace HotelRoomApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelRoomController : ControllerBase
    {
        private readonly IRepository<HotelRoom> _repository;
        private readonly IConverter<HotelRoom, HotelRoomDTO> _hotelRoomConverter;

        public HotelRoomController(IRepository<HotelRoom> repository, IConverter<HotelRoom, HotelRoomDTO> converter)
        {
            _repository = repository;
            _hotelRoomConverter = converter;
        }

        [HttpGet]
        public IEnumerable<HotelRoomDTO> GetAll()
        {
            var hotelRoomDtoList = new List<HotelRoomDTO>();

            foreach (var hotelRoom in _repository.GetAll())
            {
                var hotelRoomDto = _hotelRoomConverter.Convert(hotelRoom);
                hotelRoomDtoList.Add(hotelRoomDto);
            }
            return hotelRoomDtoList;
        }

        // GET hotelRoom/5
        [HttpGet("{id}", Name = "GetHotelRoom")]
        public IActionResult Get(int id)
        {
            var item = _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var hotelRoomDto = _hotelRoomConverter.Convert(item);
            return new ObjectResult(hotelRoomDto);
        }

        // POST hotelRooms
        [HttpPost]
        public IActionResult Post([FromBody] HotelRoomDTO hotelRoomDto)
        {
            if (hotelRoomDto == null)
            {
                return BadRequest();
            }
            try
            {
                var hotelRoom = _hotelRoomConverter.Convert(hotelRoomDto);
                var newCustomer = _repository.Add(hotelRoom);

                return CreatedAtRoute("GetHotelRoom", new { id = newCustomer.Id },
                    _hotelRoomConverter.Convert(newCustomer));
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        
        }

        [HttpDelete("{id}", Name ="DeleteHotelRoom")]
        public IActionResult DeleteHotelRoomById([FromBody] int id)
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