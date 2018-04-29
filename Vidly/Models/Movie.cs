using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Utils.Extentions;

namespace Vidly.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Customers = new List<Customer>();
        }

        #region DB mapped
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Movies2Customers> Movies2Customers { get; set; }

        [Display(Name = "Genre")]
        public Nullable<int> GenreTypeId { get; set; }
        public virtual GenreType GenreType { get; set; }

        [Display(Name = "Release date")]
        public virtual Nullable<DateTime> ReleaseDate { get; set; }

        public virtual DateTime DateAdded { get; set; }

        [Display(Name = "Number in stock")]
        public virtual int NumberInStock { get; set; }
        #endregion

        #region NotMapped
        [NotMapped]
        public IList<Customer> Customers { get; set; }

        public string GetGenreTypeName()
        {
            return GenreTypeId.HasValue ? GenreType.Name : "<unknown>";
        }

        public string GetReleaseDateToString()
        {
           return ReleaseDate.ToStringStandard() ?? "<unknown>";
        }

        #endregion
    }
}