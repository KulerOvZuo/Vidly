using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.TestData;
using Vidly.ViewModels;


namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private CustomersData customersData;

        public CustomersController()
        {
            customersData = new CustomersData();
        }

        [Route("customers")]
        public ActionResult List()
        {
            var customersView = ViewMapper.Map(customersData.GetCustomers());

            return View(customersView);
        }

        [Route("customers/{id}")]
        public ActionResult Get(int id)
        {
            var customer = customersData.GetCustomer(id);

            if (customer == null)
                return HttpNotFound();

            IList<Movie> movies =
                MoviesData.Movies.Where(m => m.CustomerIds.Contains(customer.Id)).ToList();

            return View(ViewMapper.Map(customer, movies));
        }

        protected override void Dispose(bool disposing)
        {
            customersData?.Dispose();
            base.Dispose(disposing);
        }
    }
}