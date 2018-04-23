using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.TestData
{
    public class CustomersData
    {
        private ApplicationDbContext _context;

        public CustomersData()
        {
            this._context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return this._context.Customers
                .AsNoTracking()
                .Include(c => c.MembershipType)                
                .ToList();
        }

        public Customer GetCustomer(int id)
        {
            return this._context.Customers
                .AsNoTracking()
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);
        }
    }
}