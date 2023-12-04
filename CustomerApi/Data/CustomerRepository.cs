using Microsoft.EntityFrameworkCore;
using CustomerApi.Models;

namespace CustomerApi.Data
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly CustomerApiContext db;

        public CustomerRepository(CustomerApiContext context)
        {
            db = context;
        }
        public Customer Add(Customer entity)
        {
            var newCustomer = db.Customers.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        public Customer Get(int id)
        {
            return db.Customers.FirstOrDefault(p => p.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public void Update(Customer entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = db.Customers.FirstOrDefault(p => p.CustomerId == id);
            db.Customers.Remove(customer);
            db.SaveChanges();
        }
    }
}
