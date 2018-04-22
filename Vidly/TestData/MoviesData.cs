using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.TestData
{
    public static class MoviesData
    {
        public static IList<Movie> Movies;

        public static void Initialize()
        {
            Movies = new List<Movie>
            {
                new Movie {Id = 1, Name = "Shrek" },
                new Movie {Id = 2, Name = "Wall-e" },
                new Movie {Id = 3, Name = "Sunshine"},
                new Movie {Id = 4, Name = "For whom the bell talls"}
            };
        }
    }
}