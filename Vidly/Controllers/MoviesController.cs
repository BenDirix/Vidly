using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie{Name = "Shrek"},
                new Movie{Name = "Wall-e"}
            };
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
            var movies = GetMovies();

            return View(movies);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }
    }
}