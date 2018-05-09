using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DAO;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class BaseApiController<TDao> : ApiController
        where TDao : BaseDao<ApplicationDbContext>, new()
    {
        protected TDao dao;

        public BaseApiController() : base()
        {
            this.dao = new TDao();
        }

        public BaseApiController(TDao dao) : base()
        {
            this.dao = dao;
        }

        [NonAction]
        public virtual void ResetContext()
        {
            this.dao.ResetContext();
        }

        [NonAction]
        protected override void Dispose(bool disposing)
        {
            this.dao.Dispose();
            base.Dispose(disposing);
        }
    }
}