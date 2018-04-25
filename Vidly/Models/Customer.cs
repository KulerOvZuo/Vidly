using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Movies = new List<Movie>();
        }

        #region DB mapped
        public int Id { get; set; }

        [Required][StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }

        public virtual ICollection<Movies2Customers> Movies2Customers { get; set; }
        #endregion

        #region NotMapped
        [NotMapped]
        public IList<Movie> Movies { get; set; }
        #endregion
    }
}