using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DAO
{
    public interface IDao<TContext>
        where TContext : ApplicationDbContext, new()
    {
        TContext Context { get; set; }

        void JoinWith<TDao>(TDao dao) where TDao : IDao<TContext>;
        void ResetContext();
        void Dispose();
        void SaveChanges();
    }
}