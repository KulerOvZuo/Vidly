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

            return PopulateWithMovies(ret);
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
            return PopulateWithMovies(new List<Customer> { customer }).First();
        }

        private IList<Customer> PopulateWithMovies(IList<Customer> customers)
        {
            IList<Movie> movies = this._context.Movies.AsNoTracking().ToList();

            foreach (var c in customers)
            {
                var movieIds = c.Movies2Customers.Select(mc => mc.MovieId);
                c.Movies = movies.Where(m => movieIds.Contains(m.Id)).ToList();
            }

            return customers;
        }
    }
}