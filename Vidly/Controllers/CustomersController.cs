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
        public ActionResult Details(int id)
        {
            var customer = dao.GetDetached(id);

            if (customer == null)
                return HttpNotFound();

            return View(ViewMapper.Map(customer, customer.Movies));
        }

        [Route("customers/new")]
        public ActionResult New()
        {
            var viewModel = ViewMapper.Map(null, this.dao.GetDetached<MembershipType>());

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [Route("customers/save")]
        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            this.dao.Add(viewModel.Customer);
            this.dao.SaveChanges();

            return RedirectToAction("List", "Customers");
        }

        [Route("customers/edit/{id}")]
        public ActionResult Edit(int id)
        {
            var customer = this.dao.GetDetached(id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = ViewMapper.Map(customer, this.dao.GetDetached<MembershipType>());

            return View("CustomerForm", viewModel);
        }
    }
}