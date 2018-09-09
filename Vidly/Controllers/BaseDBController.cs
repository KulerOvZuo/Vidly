using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.DAO;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class BaseDBController<TDao> : BaseController
        where TDao : BaseDao<ApplicationDbContext>, new()
    {
        protected TDao dao;

        public BaseDBController() : base()
        {
            this.dao = new TDao();
        }

        public BaseDBController(TDao dao) : base()
        {
            this.dao = dao;
        }

        public virtual void ResetContext()
        {
            this.dao.ResetContext();
        }

        protected override void Dispose(bool disposing)
        {
            this.dao.Dispose();
            base.Dispose(disposing);
        }
    }
}