using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        public CustomerController()
        {
           
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public String Get(int id)
        {
            return "yes";
        }

        [HttpPut]
        public void AddCustomer() {

        }
    }
}