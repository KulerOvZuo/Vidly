using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Utils.Extentions;
using Vidly.Utils.Validation;

namespace Vidly.Models.API
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        public Nullable<int> GenreTypeId { get; set; }

        [Display(Name = "Release date")]
        public Nullable<DateTime> ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in stock")]
        [Range(0, 100)]
        public int NumberInStock { get; set; }
    }
}