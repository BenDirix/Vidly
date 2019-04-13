using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _dbContext.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // Get /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer, CustomerDto>(customer); 
        }

        //POST /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customerDto;
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
