using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DAO
{
    public class MoviesDao : ExBaseDao<Movie>
    {
        public MoviesDao() { }
        public MoviesDao(ApplicationDbContext context) : base(context) { }

        public override IList<Movie> GetDetached()
        {
            var ret = GetWithIncludes(true)
                .ToList();

            return ret;
        }

        public override IList<Movie> Get()
        {
            var ret = GetWithIncludes(false)
                .ToList();

            return ret;
        }
        public override Movie Get(int id)
        {
            var ret = GetWithIncludes(false)
                .SingleOrDefault(m => m.Id == id);

            return PopulateWithCustomers(ret);
        }

        public override Movie GetDetached(int id)
        {
            var ret = GetWithIncludes(true)
                .SingleOrDefault(m => m.Id == id);

            return PopulateWithCustomers(ret);
        }

        public IQueryable<Movie> GetWithIncludes(bool asNoTracking = false)
        {
            var query = this._context.Movies.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

           return query
                .Include(m => m.Movies2Customers)
                .Include(m => m.GenreType);
        }

        private Movie PopulateWithCustomers(Movie movie)
        {
            if (movie == null)
                return movie;

            var customerDao = new CustomerDao(this._context);
            IList<Customer> customers = customerDao.GetWithIncludes(true).ToList();

            var customerIds = movie.Movies2Customers.Select(mc => mc.CustomerId);
            movie.Customers = customers.Where(c => customerIds.Contains(c.Id)).ToList();            

            return movie;
        }
    }
}