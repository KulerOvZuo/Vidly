using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.DAO
{
    public abstract class BaseDao<TContext> : IDisposable
        where TContext : ApplicationDbContext, new()
    {
        public TContext _context { get; protected set; }

        public BaseDao() : this(new TContext())
        {
            InitializePropertiesContext(this._context);
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
            InitializePropertiesContext(new TContext());
        }

        private void InitializePropertiesContext(TContext context)
        {
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
        }
    }
}