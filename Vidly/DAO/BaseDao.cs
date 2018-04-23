using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.DAO
{
    public class BaseDao<TContext> : IDisposable
        where TContext : ApplicationDbContext, new()
    {
        protected TContext _context;

        public BaseDao()
        {
            this._context = new TContext();
        }

        public BaseDao(TContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }

        public void ResetContext()
        {
            if (this._context == null)
                return;

            this._context.Dispose();
            this._context = new TContext();
        }
    }
}