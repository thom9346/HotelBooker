using CustomerApi.Data;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Customer;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> repository;
        private readonly IConverter<Customer, CustomerDTO> customerConverter;
        public CustomerController(IRepository<Customer> repos, IConverter<Customer, CustomerDTO> converter)
        {
            repository = repos;
            customerConverter = converter;
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


        //for some reason this does not work. Nothing is being displayed when returned, it does however to my knowledge convert correctly. 
        /*[HttpGet("{id}", Name = "GetCustomerDTO")]
        public IActionResult GetDTOById(int id)
        {
            var customer = repository.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            CustomerDTO customerDTO = customerConverter.Convert(customer);
            Console.WriteLine(customerDTO);
            Console.WriteLine(customerDTO.Name);
            return new ObjectResult(customerDTO);
        }*/

        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            var customerList = new List<Customer>(); //this should probably be a customer DTO in the future
            foreach (var customer in repository.GetAll())
            {
                customerList.Add(customer);
            }
            return customerList;
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer) {
            if (customer == null)
            {
                return BadRequest();
            }

            var newCustomer = repository.Add(customer);

            return CreatedAtRoute("GetCustomer", new { id = newCustomer }, newCustomer);
        }

        [HttpPut("{id}", Name = "UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] string customer)
        {
            //takes Json from PaymentApi. Change when RabbitMQ is running. 
            Console.WriteLine(customer);
            if (customer == null)
            {
                Console.WriteLine("Bad request in update Customer");
                return BadRequest();
            }
            /* Console.WriteLine(customer.Name + "name from udpate");
             Customer getCustomer = repository.Get(customer.CustomerId);
             getCustomer.Name = customer.Name;
             getCustomer.PhoneNr = customer.PhoneNr;
             getCustomer.Age = customer.Age;
             getCustomer.Email = customer.Email;
             getCustomer.Balance = customer.Balance;

             repository.Delete(customer.CustomerId);
             repository.Add(getCustomer);*/

            //return new ObjectResult(getCustomer);
            return new ObjectResult(customer);

        }

        [HttpDelete("{id}", Name="DeleteCustomer")]
        public IActionResult DeleteCustomerById([FromBody] int id)
        {
            if (repository.Get(id) == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok();
        }
    }
}