using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DAO;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : BaseController<MoviesDao>
    {
        [HttpGet]
        [Route("movies")]
        public ActionResult List()
        {
            var moviesView = ViewMapper.Map(this.dao.GetDetached());

            return View(moviesView);
        }

        [HttpGet]
        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = this.dao.GetDetached(id);

            if (movie == null)
                return HttpNotFound();

            return View(ViewMapper.Map(movie, movie.Customers));
        }

        [HttpGet]
        [Route("movies/new")]
        public ActionResult New()
        {
            var viewModel = ViewMapper.Map(new Movie(), this.dao.GetDetached<GenreType>());

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [Route("movies/save")]
        public ActionResult Save(MovieModelViewForm viewModel)
        {
            var movie = viewModel.Movie;
            if (movie.Id <= 0)
            {
                movie.DateAdded = DateTime.Now;
                this.dao.Add(movie);
            }                
            else
            {
                var movieInDB = this.dao.Get(movie.Id);

                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate ;
                movieInDB.GenreTypeId = movie.GenreTypeId;
                movieInDB.NumberInStock = movie.NumberInStock;
            }

            this.dao.SaveChanges();

            return RedirectToAction("list", "movies");
        }

        [HttpGet]
        [Route("movies/edit/{id}")]
        public ActionResult Edit(int id)
        {
            var movie = this.dao.GetDetached(id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = ViewMapper.Map(movie, this.dao.GetDetached<GenreType>());

            return View("MovieForm", viewModel);
        }
    }
}