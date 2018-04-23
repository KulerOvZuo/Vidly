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
    public class MoviesController : Controller
    {
        private CustomersData customersData;

        public MoviesController()
        {
            customersData = new CustomersData();
        }

        [Route("movies")]
        public ActionResult List()
        {
            var moviesView = ViewMapper.Map(MoviesData.Movies);

            return View(moviesView);
        }

        [Route("movies/{id}")]
        public ActionResult Get(int id)
        {
            var movie = MoviesData.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            IList<Customer> customers =
                customersData.GetCustomers().Where(c => movie.CustomerIds.Contains(c.Id)).ToList();

            return View(ViewMapper.Map(movie, customers));
        }
    }
}