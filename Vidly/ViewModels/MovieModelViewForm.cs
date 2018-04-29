using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieModelViewForm
    {
        public Movie Movie { get; set; }
        public IList<GenreType> GenreTypes { get; set; }
    }
}