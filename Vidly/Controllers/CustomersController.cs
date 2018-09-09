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
        [HttpGet]
        [Route("customers")]
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = dao.GetDetached(id);

            if (customer == null)
                return HttpNotFound();

            return View(ViewMapper.Map(customer, customer.Movies));
        }

        [HttpGet]
        [Route("customers/new")]
        public ActionResult New()
        {
            var viewModel = ViewMapper.Map(new Customer(), this.dao.GetDetached<MembershipType>());

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("customers/save")]
        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            var customer = viewModel.Customer;

            if (!ModelState.IsValid)
            {
                var returnViewModel = ViewMapper.Map(customer, this.dao.GetDetached<MembershipType>());
                return View("CustomerForm", returnViewModel);
            }
           
            if(customer.Id <= 0)
                this.dao.Add(customer);
            else
            {
                var customerInDB = this.dao.Get(customer.Id);

                customerInDB.Name = customer.Name;
                customerInDB.BirthDate = customer.BirthDate;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            this.dao.SaveChanges();

            return RedirectToAction("list", "customers");
        }

        [HttpGet]
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