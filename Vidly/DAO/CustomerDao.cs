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
            var ret = GetWithIncludes(true)
                .ToList();

            return ret;
        }

        public override Customer Get(int id)
        {
            var ret = GetWithIncludes(false)
                .SingleOrDefault(c => c.Id == id);

            return PopulateWithMovies(ret);
        }

        public override Customer GetDetached(int id)
        {
            var ret = GetWithIncludes(true)
                .SingleOrDefault(c => c.Id == id);

            return PopulateWithMovies(ret);
        }

        public IQueryable<Customer> GetWithIncludes(bool asNoTracking = false)
        {
            var query = this._context.Customers.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return query
                .Include(c => c.MembershipType)
                .Include(c => c.Movies2Customers);
        }

        private Customer PopulateWithMovies(Customer customer)
        {
            if (customer == null)
                return customer;

            var moviesDao = new MoviesDao(this.Context);
            IList<Movie> movies = moviesDao.GetWithIncludes(true).ToList();

            var movieIds = customer.Movies2Customers.Select(mc => mc.MovieId);
            customer.Movies = movies.Where(m => movieIds.Contains(m.Id)).ToList();

            return customer;
        }

        public override IList<Customer> Get()
        {
            var ret = GetWithIncludes(false)
                .ToList();

            return ret;
        }
    }
}