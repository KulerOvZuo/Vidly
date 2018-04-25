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
            return DetachedWithIncludes()
                .ToList();
        }

        public Customer GetDetached(int id)
        {
            return DetachedWithIncludes()
                .SingleOrDefault(c => c.Id == id);
        }

        private IQueryable<Customer> DetachedWithIncludes()
        {
            return null;
            //return this._context.Customers
            //    .AsNoTracking()
            //    .Include(c => c.MembershipType)
            //    .Include(c => c.Movies)
            //    .Include(c => c.Movies.Select(m => m.Customers))
            //    ;
        }
    }
}