using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DAO
{
    public class Movies2CustomersDao : ExBaseDao<Movies2Customers>
    {
        public Movies2CustomersDao() { }
        public Movies2CustomersDao(ApplicationDbContext context) : base(context) { }

        public override IList<Movies2Customers> GetDetached()
        {
            var ret = GetWithIncludes(true)
                .ToList();

            return ret;
        }

        public override IList<Movies2Customers> Get()
        {
            var ret = GetWithIncludes(false)
                .ToList();

            return ret;

        }
        public override Movies2Customers Get(int id)
        {
            var ret = GetWithIncludes(false)
                .SingleOrDefault(c => c.Id == id);

            return ret;
        }

        public override Movies2Customers GetDetached(int id)
        {
            var ret = GetWithIncludes(true)
                .SingleOrDefault(c => c.Id == id);

            return ret;
        }

        public IQueryable<Movies2Customers> GetWithIncludes(bool asNoTracking = false)
        {
            var query = this._context.Movies2Customers.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }
    }
}