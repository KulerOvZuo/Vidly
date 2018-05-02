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
        [Route("api/customers")]
        public IEnumerable<Customer> GetCustomers()
        {
            this.dao.Context.Configuration.ProxyCreationEnabled = false;
            return this.dao.GetDetached();
        }

        // GET /api/customers/1
        [HttpGet]
        [Route("api/customers/{id}")]
        public Customer GetCustomer(int id)
        {
            var customer = this.dao.GetDetached(id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        // POST /api/customers
        [HttpPost]
        [Route("api/customers")]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            customer.MembershipType = null;

            this.dao.Add(customer);
            this.dao.SaveChanges();

            return this.dao.GetDetached(customer.Id);
        }

        // PUT /api/customers/1
        [HttpPut]
        [Route("api/customers/{id}")]
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
        [Route("api/customers/{id}")]
        public void DeleteCustomer(int id)
        {
            var customerinDB = this.dao.Get(id);

            if (customerinDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this.dao.Remove(customerinDB);
            this.dao.SaveChanges();
        }
    }
}
