using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.DAO;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class BaseController<TDao> : Controller
        where TDao : BaseDao<ApplicationDbContext>, new()
    {
        protected TDao dao;

        public BaseController()
        {
            this.dao = new TDao();
        }

        public BaseController(TDao dao)
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