using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DAO;
using Vidly.Models;
using Vidly.Models.API;

namespace Vidly.Controllers.API
{
    public class MoviesController : BaseApiController<MoviesDao>
    {
        [HttpGet]
        [Route("api/Movies")]
        public IHttpActionResult GetMovies()
        {
            var movies = this.dao.GetDetached().Select(c => Mapper.Map<Movie, MovieDTO>(c));
            return Ok(movies);
        }

        [HttpGet]
        [Route("api/Movies/{id}")]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = this.dao.GetDetached(id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDTO>(movie));
        }

        [HttpPost]
        [Route("api/Movies")]
        public IHttpActionResult CreateMovie(MovieDTO MovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDTO, Movie>(MovieDto);
            this.dao.Add(movie);
            this.dao.SaveChanges();

            movie = this.dao.GetDetached(movie.Id);

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), 
                Mapper.Map<Movie, MovieDTO>(movie));
        }

        [HttpPut]
        [Route("api/Movies/{id}")]
        public IHttpActionResult UpdateMovie(int id, MovieDTO MovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = this.dao.Get(id);

            if (movieInDB == null)
                return NotFound();

            Mapper.Map<MovieDTO, Movie>(MovieDto, movieInDB);
            this.dao.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/Movies/{id}")]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDB = this.dao.Get(id);

            if (movieInDB == null)
                return NotFound();

            this.dao.Remove(movieInDB);
            this.dao.SaveChanges();

            return Ok();
        }
    }
}
