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
        [Route("movies")]
        public ActionResult List()
        {
            var moviesView = ViewMapper.Map(this.dao.GetDetached());

            return View(moviesView);
        }

        [Route("movies/details/{id}")]
        public ActionResult Get(int id)
        {
            var movie = this.dao.GetDetached(id);

            if (movie == null)
                return HttpNotFound();

            return View(ViewMapper.Map(movie, movie.Customers));
        }
    }
}