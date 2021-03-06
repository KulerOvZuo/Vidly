﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DAO;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : BaseDBController<MoviesDao>
    {
        [HttpGet]
        [Route("movies")]
        public ActionResult List()
        {
            var moviesView = ViewMapper.Map(this.dao.GetDetached());

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List", moviesView);

            return View("AnonymousList", moviesView);
        }

        [HttpGet]
        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = this.dao.GetDetached(id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = ViewMapper.Map(movie, movie.Customers);

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("Details", viewModel);

            return View("AnonymousDetails", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.CanManageMovies)]
        [Route("movies/new")]
        public ActionResult New()
        {
            var genres = MemoryCache.Data<GenreType>(this.dao);
            var viewModel = ViewMapper.Map(new Movie(), genres);

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        [Route("movies/save")]
        public ActionResult Save(MovieModelViewForm viewModel)
        {
            if (!ModelState.IsValid)
            {
                var genres = MemoryCache.Data<GenreType>(this.dao);
                var retViewModel = ViewMapper.Map(viewModel.Movie, genres);
                return View("MovieForm", retViewModel);
            }

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

                movieInDB.NumberAvailable = movieInDB.NumberInStock;
            }

            this.dao.SaveChanges();

            return RedirectToAction("list", "movies");
        }

        [HttpGet]
        [Route("movies/edit/{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = this.dao.GetDetached(id);

            if (movie == null)
                return HttpNotFound();

            var genres = MemoryCache.Data<GenreType>(this.dao);
            var viewModel = ViewMapper.Map(movie, genres);

            return View("MovieForm", viewModel);
        }
    }
}