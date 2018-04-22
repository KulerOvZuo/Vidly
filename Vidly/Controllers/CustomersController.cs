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
        [Route("customers")]
        public ActionResult List()
        {
            var customersView = ViewMapper.Map(CustomersData.Customers);

            return View(customersView);
        }

        [Route("customers/{id}")]
        public ActionResult Get(int id)
        {
            var customer = CustomersData.Customers.FirstOrDefault(m => m.Id == id);

            if (customer == null)
                return HttpNotFound();

            IList<Movie> movies =
                MoviesData.Movies.Where(m => m.CustomerIds.Contains(customer.Id)).ToList();

            return View(ViewMapper.Map(customer, movies));
        }
    }
}