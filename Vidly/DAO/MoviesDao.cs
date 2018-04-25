using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DAO
{
    public class MoviesDao : BaseDao<ApplicationDbContext>
    {
        public IList<Movie> GetDetached()
        {
            return DetachedWithIncludes()
                .ToList();
        }

        public Movie GetDetached(int id)
        {
            return DetachedWithIncludes()
                .SingleOrDefault(m => m.Id == id);
        }

        private IQueryable<Movie> DetachedWithIncludes()
        {
            return null;
            //return this._context.Movies
            //    .AsNoTracking()
            //    .Include(m => m.Customers)
            //    .Include(m => m.Customers.Select(c => c.MembershipType))
            //    ;
        }

        private IEnumerable<Customer> LoadCustomers()
        {
            return this._context.Customers
                .AsNoTracking()
                .Include(c => c.MembershipType)
                ;
        }
    }
}