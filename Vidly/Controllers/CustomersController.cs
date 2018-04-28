using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DAO;
using Vidly.Models;
using Vidly.ViewModels;


namespace Vidly.Controllers
{
    public class CustomersController : BaseController<CustomerDao>
    {
        [Route("customers")]
        public ActionResult List()
        {
            var customersView = ViewMapper.Map(dao.GetDetached());

            return View(customersView);
        }

        [Route("customers/details/{id}")]
        public ActionResult Get(int id)
        {
            var customer = dao.GetDetached(id);

            if (customer == null)
                return HttpNotFound();

            return View(ViewMapper.Map(customer, customer.Movies));
        }
    }
}