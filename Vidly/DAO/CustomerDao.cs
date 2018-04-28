using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DAO
{
    public class CustomerDao : ExBaseDao<Customer>
    {
        public CustomerDao() { }
        public CustomerDao(ApplicationDbContext context) : base(context) { }

        public override IList<Customer> GetDetached()
        {
            var ret = DetachedWithIncludes()
                .ToList();

            return ret;
        }

        public override Customer GetDetached(int id)
        {
            var ret = DetachedWithIncludes()
                .SingleOrDefault(c => c.Id == id);

            return PopulateWithMovies(ret);
        }

        public IQueryable<Customer> DetachedWithIncludes()
        {
            return this._context.Customers
                .AsNoTracking()
                .Include(c => c.MembershipType)
                .Include(c => c.Movies2Customers);
        }

        private Customer PopulateWithMovies(Customer customer)
        {
            IList<Movie> movies;
            using (var moviesDao = new MoviesDao(this._context))
            {
                movies = moviesDao.DetachedWithIncludes().ToList();
            }                

            var movieIds = customer.Movies2Customers.Select(mc => mc.MovieId);
            customer.Movies = movies.Where(m => movieIds.Contains(m.Id)).ToList();

            return customer;
        }
    }
}