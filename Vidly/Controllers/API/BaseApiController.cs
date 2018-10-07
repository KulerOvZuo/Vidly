using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Vidly.DAO;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class BaseApiController<TDao> : ApiController
        where TDao : BaseDao<ApplicationDbContext>, new()
    {
        private IList<IDao<ApplicationDbContext>> daos = null;
        protected IList<IDao<ApplicationDbContext>> GetDaos()
        {
            if (daos == null)
                daos = this.GetType().GetProperties(BindingFlags.GetField | BindingFlags.GetProperty |
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetValue(this) is IDao<ApplicationDbContext>)
                .Select(p => p.GetValue(this) as IDao<ApplicationDbContext>)
                .Where(d => d != this._dao).ToList();

            return daos;
        }

        protected TDao _dao { get; set; }

        public BaseApiController() : this(new TDao())
        {

        }

        public BaseApiController(TDao dao) : base()
        {
            this._dao = dao;
            GetDaos();
            JoinDaos();
        }

        [NonAction]
        public virtual void ResetContext()
        {
            this._dao.ResetContext();
            foreach (var dao in daos)
                dao.ResetContext();
        }

        [NonAction]
        protected override void Dispose(bool disposing)
        {
            this._dao.Dispose();
            foreach (var dao in daos)
                dao.Dispose();

            base.Dispose(disposing);
        }

        [NonAction]
        protected void JoinDaos()
        {
            foreach (var dao in daos)
                this._dao.JoinWith(dao);
        }

    }
}