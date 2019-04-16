using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // Get /api/customers
        public IEnumerable<CustomerDto> GetCustomers(string query = null)
        {
            var customersQuery = _dbContext.Customers.Include(c => c.MembershipType);
            if(!string.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            
            return customersQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // Get /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer)); 
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            Mapper.Map(customerDto, customerInDb);  // No need for <CustomerDto, Customer> cause it knows the types from the arguments          

            _dbContext.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
        }
    }
}
