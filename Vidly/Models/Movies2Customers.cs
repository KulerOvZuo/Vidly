using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movies2Customers
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Movie")]
        [Required]
        public int MovieId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Customer")]
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime DateRented { get; set; }
        public Nullable<DateTime> DateReturned { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Customer Customer { get; set; }
    }
}