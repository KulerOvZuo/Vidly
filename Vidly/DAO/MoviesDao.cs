﻿using System;
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
            var ret = DetachedWithIncludes()
                .ToList();

            return ret;
        }

        public Movie GetDetached(int id)
        {
            var ret = DetachedWithIncludes()
                .SingleOrDefault(m => m.Id == id);

            return PopulateWithCustomers(ret);
        }

        private IQueryable<Movie> DetachedWithIncludes()
        {
            return this._context.Movies
                .AsNoTracking()
                .Include(m => m.Movies2Customers);
        }

        private Movie PopulateWithCustomers(Movie movie)
        {
            IList<Customer> customers = this._context.Customers.AsNoTracking()
                .Include(c => c.MembershipType)
                .Include(m => m.Movies2Customers)
                .ToList();

            var customerIds = movie.Movies2Customers.Select(mc => mc.CustomerId);
            movie.Customers = customers.Where(c => customerIds.Contains(c.Id)).ToList();            

            return movie;
        }
    }
}