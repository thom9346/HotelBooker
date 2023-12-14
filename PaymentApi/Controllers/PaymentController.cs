using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using SharedModels.Customer;
using PaymentApi.Infrastructure;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        public PaymentController(IMessagePublisher publisher)
        {
            _messagePublisher = publisher;

        }

        [HttpPut]
        public void EditBalanceById(int customerId, int amount)
        {
            if (customerId > 0 && amount != 0)
            {
                Console.WriteLine("Payment sent" + amount);
                _messagePublisher.PublishPaymentCreatedMessage(customerId, amount);
            }




        }

    }
}