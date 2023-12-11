using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedModels;
using System.Net.Http.Json;
using System.Net.Http;

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
        public async void EditBalanceById(int customerId, int amount)
        {
            if (amount > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://customerapi/Customer/" + customerId);

                var result = client.GetAsync(client.BaseAddress).Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine("result: " + result);
                var json = (JObject)JsonConvert.DeserializeObject(result);
            
                CustomerDTO newCustomer = new CustomerDTO() { CustomerId = (int)json["customerId"], Name = (string)json["name"], PhoneNr = (string)json["phoneNr"], Email = (string) json["email"], Age = (int)json["age"], Balance = (int)json["balance"]};
                newCustomer.Balance += amount;
                Console.WriteLine("New customer balance:" + newCustomer.Balance);




                HttpClient newClient = new HttpClient();
                newClient.BaseAddress = new Uri("http://customerapi/Customer/" + customerId);
                string jsonContent = JsonConvert.SerializeObject(newCustomer);
                var respoonse = await newClient.PutAsJsonAsync("UpdateCustomer", jsonContent);


                Console.WriteLine(respoonse);
            
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