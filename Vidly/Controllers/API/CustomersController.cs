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
        public IHttpActionResult GetCustomers(string query = null)
        {
            var queryable = this._dao.GetDetached().AsQueryable();

            if (!string.IsNullOrEmpty(query))
                queryable = queryable.Where(c => c.Name.Contains(query));

            var customers = queryable.Select(c => Mapper.Map<Customer, CustomerDTO>(c));
            return Ok(customers);
        }

        [HttpGet]
        [Route("api/customers/{id}")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = this._dao.GetDetached(id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
        }

        [HttpPost]
        [Route("api/customers")]
        public IHttpActionResult CreateCustomer(CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);
            this._dao.Add(customer);
            this._dao.SaveChanges();

            customer = this._dao.GetDetached(customer.Id);

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), 
                Mapper.Map<Customer, CustomerDTO>(customer));
        }

        [HttpPut]
        [Route("api/customers/{id}")]
        public IHttpActionResult UpdateCustomer(int id, CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerinDB = this._dao.Get(id);

            if (customerinDB == null)
                return NotFound();

            Mapper.Map<CustomerDTO, Customer>(customerDto, customerinDB);
            this._dao.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/customers/{id}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerinDB = this._dao.Get(id);

            if (customerinDB == null)
                return NotFound();

            this._dao.Remove(customerinDB);
            this._dao.SaveChanges();

            return Ok();
        }
    }
}
