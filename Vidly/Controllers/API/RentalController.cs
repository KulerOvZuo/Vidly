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
    public class RentalController : BaseApiController<Movies2CustomersDao>
    {
        CustomerDao customerDao { get; set; }  = new CustomerDao();
        MoviesDao moviesDao { get; set; } = new MoviesDao();

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDTO rental)
        {          
            if (rental.MovieIds == null || !rental.MovieIds.Any())
                return BadRequest($"No movies selected");

            var customer = customerDao.GetDetached(rental.CustomerId);
            if (customer == null)
                return BadRequest($"Invalid CustomerId: {rental.CustomerId}");

            var movies = moviesDao.Get().Where(m => rental.MovieIds.Contains(m.Id)).ToList();
            foreach(var movieId in rental.MovieIds)
            {
                var dbMovie = movies.SingleOrDefault(m => m.Id == movieId);

                if (dbMovie == null)
                    return BadRequest($"Invalid MovieId: {movieId}");

                if(dbMovie.NumberAvailable <= 0)
                    return BadRequest($"Movie not available: '{dbMovie.Name}'");

                dbMovie.NumberAvailable--;
            }

            var dateRented = DateTime.Now;
            IList<Movies2Customers> rentals = rental.MovieIds.Select(m => new Movies2Customers
            {
                CustomerId = rental.CustomerId,
                DateRented = dateRented,
                MovieId = m
            }).ToList();

            this._dao.AddRange(rentals);
            this._dao.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            customerDao.Dispose();
            moviesDao.Dispose();
            base.Dispose(disposing);
        }
    }
}
