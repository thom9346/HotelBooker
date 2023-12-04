using SharedModels;

namespace CustomerApi.Models
{
    public class CustomerConverter : IConverter<Customer, CustomerDTO>
    {

        public Customer Convert(CustomerDTO sharedCustomer)
        {
            return new Customer
            {
                CustomerId = sharedCustomer.CustomerId,
                Name = sharedCustomer.Name,
                PhoneNr = sharedCustomer.PhoneNr,
                Email = sharedCustomer.Email,
                Age = sharedCustomer.Age,
                Balance = 0 //careful
            };
        }

        public CustomerDTO Convert(Customer hiddenCustomer)
        {
            return new CustomerDTO
            {
                CustomerId = hiddenCustomer.CustomerId,
                Name = hiddenCustomer.Name,
                PhoneNr = hiddenCustomer.PhoneNr,
                Email = hiddenCustomer.Email,
                Age = hiddenCustomer.Age
            };
        }
    }
}
