using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerViewModelDetailed
    {
        public Customer Customer { get; set; }
        public IList<Movie> Movies { get; set; }
    }
}