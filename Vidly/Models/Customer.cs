using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Utils.Extentions;
using Vidly.Utils.Validation;

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

        [Required(ErrorMessage = "Please enter customer's 'name'.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Subscribed to Newsletter?")]
        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [Min18YearsIfAMember]
        public Nullable<DateTime> BirthDate { get; set; }
        
        [Display(Name = "Membership type")]
        public byte MembershipTypeId { get; set; }

        public MembershipType MembershipType { get; set; }

        public virtual ICollection<Movies2Customers> Movies2Customers { get; set; }
        #endregion

        #region NotMapped
        [NotMapped]
        public IList<Movie> Movies { get; set; }

        public string GetBirthDateToString()
        {
            return BirthDate.ToStringStandard() ?? "<unknown>";
        }

        #endregion
    }
}