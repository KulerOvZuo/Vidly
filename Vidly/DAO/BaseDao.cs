using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.DAO
{
    public abstract class BaseDao<TContext> : IDisposable, IDao<TContext>
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
            set => this._context = value;
        }

        public void JoinWith<TDao>(TDao underlingDao) where TDao : IDao<TContext>
        {
            underlingDao.Context = this._context;
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
            try
            {
                this._context.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors.Select(v => $"{v.PropertyName} : {v.ErrorMessage}"));
                throw new DbEntityValidationException(
                    string.Join(", ", errors),
                    ex);
            }
            catch(Exception)
            {
                throw;
            }           
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