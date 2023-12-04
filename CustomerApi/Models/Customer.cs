namespace CustomerApi.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string PhoneNr { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public int Balance { get; set; }
    }
}