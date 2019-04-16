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
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public NewRentalsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // Get /api/newRentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental){
            if(newRental.MovieIds.Count == 0)
                return BadRequest("No Movie Ids were given.");

            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            if(customer == null)
                return BadRequest("Customer Id is not valid");

            var movies = _dbContext.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
            if(movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movie Ids are invalid");

            foreach(var movie in movies){
                if(movie.NumberAvailable == 0)
                    return BadRequest($"{movie.Name} is not available");
                movie.NumberAvailable--;
                var rental = new Rental{
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _dbContext.Rentals.Add(rental);
            }
            _dbContext.SaveChanges();
            return Ok("");
        }
    }
}
