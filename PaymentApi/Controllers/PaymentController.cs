using Microsoft.AspNetCore.Mvc;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {


        public PaymentController()
        {

        }

        [HttpPut]
        public void EditBalanceById(int customerId, int amount)
        {
            if (amount > 0)
            {
                //CustomerApi.GetCustomerById.balance += amount;
            }
            else if (amount < 0)
            {
                //CustomerApi.GetCustomerById.balance -= amount;
            }
            else
            {
                //badRequest
            }


        }

    }
}