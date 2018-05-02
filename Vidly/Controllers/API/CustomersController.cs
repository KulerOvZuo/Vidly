using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DAO;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : BaseApiController<CustomerDao>
    {
        // GET /api/customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return this.dao.GetDetached();
        }

        // GET /api/customers/1
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            var customer = this.dao.GetDetached(id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        // POST /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            this.dao.Add(customer);
            this.dao.SaveChanges();

            return customer;
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerinDB = this.dao.Get(id);

            if (customerinDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerinDB.Name = customer.Name;
            customerinDB.BirthDate = customer.BirthDate;
            customerinDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerinDB.MembershipTypeId = customer.MembershipTypeId;

            this.dao.SaveChanges();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerinDB = this.dao.GetDetached(id);

            if (customerinDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this.dao.Remove(customerinDB);
            this.dao.SaveChanges();
        }
    }
}
