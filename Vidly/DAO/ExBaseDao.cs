using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.DAO
{
    public abstract class ExBaseDao<TEntity> : BaseDao<ApplicationDbContext>
        where TEntity : class, new()
    {
        public ExBaseDao() : base() { }
        public ExBaseDao(ApplicationDbContext context) : base(context) { }

        public abstract IList<TEntity> GetDetached();
        public abstract TEntity GetDetached(int id);
        public abstract TEntity Get(int id);

        public virtual void Add(TEntity entity)
        {
            DbSet<TEntity>().Add(entity);
        }
    }
}