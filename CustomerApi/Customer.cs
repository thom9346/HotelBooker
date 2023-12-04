namespace CustomerApi
{
    public class Customer
    {
        public int CustomerId;

        public string Name;

        public string PhoneNr;

        public string Email;

        public int Age;

        public int Balance;

        public Customer(int id, string name, string phone, string email, int age, int bal)
        {
            CustomerId = id;
            Name = name;
            PhoneNr = phone;
            Email = email;
            Age = age;
            Balance = bal;
        }
    }
}