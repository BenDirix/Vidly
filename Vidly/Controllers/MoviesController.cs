using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _dbContext;
        public MoviesController(){
            _dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing){
            _dbContext.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {

            Movie movie = new Movie() { Name = "Shrek" };
            List<Customer> customers = new List<Customer>
            {
                new Customer{Name = "Customer 1"},
                new Customer{Name = "Customer 2"}
            };

            var movieViewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };


            //ViewData["Movie"] = movie;
            //ViewBag.Movie = movie;

            return View(movieViewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content($"id={id}");
        }

        //GET: Movies
        public ActionResult Index()
        {
            var movies = _dbContext.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }
        public ActionResult Details(int id)
        {
            var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if(movie == null)
                return HttpNotFound();
            return View(movie);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }
    }
}