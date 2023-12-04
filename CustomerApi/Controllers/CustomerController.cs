using CustomerApi.Data;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> repository;
        public CustomerController(IRepository<Customer> repos)
        {
            repository = repos;
        }


        //gets the specified Customer by ID in repos
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(int id)
        {
            var customer = repository.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer) {
            if (customer == null)
            {
                return BadRequest();
            }

            var newCustomer = repository.Add(customer);

            //I do not know what this does but I yoinked 
            
            return CreatedAtRoute("GetCustomer", new { id = newCustomer }, newCustomer);
        }
    }
}