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
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Customer Customer { get; set; }
    }
}