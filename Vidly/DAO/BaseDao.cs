using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        protected TContext _context;

        public BaseDao() : this(new TContext())
        {
            InitializePropertiesContext(this._context);
        }

        public BaseDao(TContext context)
        {
            this._context = context;
        }

        public TContext Context
        {
            get => this._context;
        }

        private void InitializePropertiesContext(TContext context)
        {
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void ResetContext()
        {
            if (this._context == null)
                return;

            this._context.Dispose();

            this._context = new TContext();
            InitializePropertiesContext(this._context);
        }
        
        public DbSet<TEntity> DbSet<TEntity>() where TEntity : class, new()
        {
            return this._context.Set<TEntity>();
        }

        public IList<TEntity> GetDetached<TEntity>() where TEntity : class, new()
        {
            return this._context.Set<TEntity>().AsNoTracking().ToList();
        }
    }
}