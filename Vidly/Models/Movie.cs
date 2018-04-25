using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        //public Movie()
        //{
        //    this.Customers = new List<Customer>();
        //}

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Movies2Customers> Movies2Customers { get; set; }     
    }
}