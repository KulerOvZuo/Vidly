using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public static class ViewMapper
    {
        public static MoviesViewModel Map(IList<Movie> movies)
        {
            return new MoviesViewModel { Movies = movies };
        }

        public static MovieViewModelDetailed Map(Movie movie, IList<Customer> customers)
        {
            return new MovieViewModelDetailed { Movie = movie, Customers = customers };
        }

        public static CustomersViewModel Map(IList<Customer> customers)
        {
            return new CustomersViewModel { Customers = customers };
        }

        public static CustomerViewModelDetailed Map(Customer customer, IList<Movie> movies)
        {
            return new CustomerViewModelDetailed { Customer = customer, Movies = movies };
        }
    }
}