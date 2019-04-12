using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //private List<Customer> customers = new List<Customer>();
        private ApplicationDbContext _dbContext;  
        
        public CustomersController(){
            _dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing){
            _dbContext.Dispose();
        }
        //private IEnumerable<Customer> GetCustomers(){
        //    return new List<Customer>{
        //        new Customer{Name = "John Smith", Id = 1},
        //        new Customer{Name = "Mary Williams", Id = 2},
        //    };
        //}        
        // GET: Customers
        public ActionResult Index()
        {
            //var customers = GetCustomers();

            // ToList will make the query immediately executed
            var customers = _dbContext.Customers.ToList();
            return View(customers);
        }

        public ActionResult Details(int id){
            // SingleOrDefault will make the query immediately executed
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customer != null)
                return View(customer);
            else
                return HttpNotFound();
        }
    }
}