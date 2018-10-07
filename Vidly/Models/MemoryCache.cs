using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using System.Collections.Specialized;
using Vidly.DAO;

namespace Vidly.Models
{
    public class MemoryCache : System.Runtime.Caching.MemoryCache
    {
        public MemoryCache(string name, NameValueCollection config = null) : base(name, config)
        {
        }

        public static IList<TEntity> Data<TEntity>(BaseDao<ApplicationDbContext> dao)
            where TEntity : class, new()
        {
            string key = typeof(TEntity).Name;

            if (Default[key] == null)
                Default[key] = dao.GetDetached<TEntity>();

            return Default[key] as IList<TEntity>;
        }
    }
}