using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
       
        // GET: Customers
        public ActionResult Index()
        {
            // ToList will make the query immediately executed
            var customers = _dbContext.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            // SingleOrDefault will make the query immediately executed
            var customer = _dbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if(customer != null)
                return View(customer);
            else
                return HttpNotFound();
        }

        public ActionResult New()
        {
            var membershipTypes = _dbContext.MembershipTypes.ToList();
            var ViewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
            };
            return View("CustomerForm", ViewModel);
        }
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var customerViewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _dbContext.MembershipTypes.ToList()
                };

                return View("CustomerForm", customerViewModel);
            }

            if(customer.Id == 0)
                _dbContext.Customers.Add(customer);
            else
            {
                var customerInDb = _dbContext.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel { Customer = customer, MembershipTypes = _dbContext.MembershipTypes.ToList() };
            return View("CustomerForm", viewModel);
        }
    }
}