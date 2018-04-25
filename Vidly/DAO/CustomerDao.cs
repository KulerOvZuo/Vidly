using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DAO
{
    public class CustomerDao : BaseDao<ApplicationDbContext>
    {
        public IList<Customer> GetDetached()
        {
            var ret = DetachedWithIncludes()
                .ToList();

            return ret;
        }

        public Customer GetDetached(int id)
        {
            var ret = DetachedWithIncludes()
                .SingleOrDefault(c => c.Id == id);

            return PopulateWithMovies(ret);
        }

        private IQueryable<Customer> DetachedWithIncludes()
        {
            return this._context.Customers
                .AsNoTracking()
                .Include(c => c.MembershipType)
                .Include(c => c.Movies2Customers);
        }

        private Customer PopulateWithMovies(Customer customer)
        {
            IList<Movie> movies = this._context.Movies.AsNoTracking()
                .Include(m => m.Movies2Customers)
                .ToList();

            var movieIds = customer.Movies2Customers.Select(mc => mc.MovieId);
            customer.Movies = movies.Where(m => movieIds.Contains(m.Id)).ToList();

            return customer;
        }
    }
}