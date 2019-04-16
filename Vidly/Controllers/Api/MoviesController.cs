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
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET /api/movies
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _dbContext.Movies.Include(m => m.Genre).Where(m => m.NumberAvailable > 0);
            if(!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if(movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies/1
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri+"/"+movie.Id), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if(movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(movieDto, movieInDb);
            _dbContext.SaveChanges();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
            if(movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }
    }
}
