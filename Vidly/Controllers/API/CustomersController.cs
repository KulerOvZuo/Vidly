using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DAO;
using Vidly.Models;
using Vidly.Models.API;

namespace Vidly.Controllers.API
{
    public class CustomersController : BaseApiController<CustomerDao>
    {
        [HttpGet]
        [Route("api/customers")]
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            this.dao.Context.Configuration.ProxyCreationEnabled = false;
            return this.dao.GetDetached().Select(c => Mapper.Map<Customer, CustomerDTO>(c));
        }

        [HttpGet]
        [Route("api/customers/{id}")]
        public CustomerDTO GetCustomer(int id)
        {
            var customer = this.dao.GetDetached(id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer, CustomerDTO>(customer);
        }

        [HttpPost]
        [Route("api/customers")]
        public CustomerDTO CreateCustomer(CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);
            this.dao.Add(customer);
            this.dao.SaveChanges();

            customer = this.dao.GetDetached(customer.Id);
            return Mapper.Map<Customer, CustomerDTO>(customer);
        }

        [HttpPut]
        [Route("api/customers/{id}")]
        public void UpdateCustomer(int id, CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerinDB = this.dao.Get(id);

            if (customerinDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<CustomerDTO, Customer>(customerDto, customerinDB);
            this.dao.SaveChanges();
        }

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
