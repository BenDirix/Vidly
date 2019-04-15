using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _dbContext;
        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
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

            return View(movieViewModel);
        }

        //GET: Movies
        public ActionResult Index()
        {
            if(User.IsInRole(RoleName.CanManageMovies))
                return View();

            return View("ReadOnlyList");
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
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _dbContext.Genres.ToList();
            var movieViewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres,
            };
            return View("MovieForm", movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid){
                var movieViewModel = new MovieFormViewModel{
                    Movie = movie,
                    Genres = _dbContext.Genres.ToList(),
                };
                return View("MovieForm", movieViewModel);
            }
            if(movie.Id == 0){
                movie.DateAdded = DateTime.Today;
                _dbContext.Movies.Add(movie);
            }
            else {
                var movieInDb = _dbContext.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id){
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if(movie == null)
                return HttpNotFound();

            var movieViewModel = new MovieFormViewModel { Movie = movie, Genres = _dbContext.Genres.ToList() };
            return View("MovieForm", movieViewModel);
        }
    }
}