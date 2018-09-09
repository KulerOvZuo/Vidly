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
    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's 'name'.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [DataType(DataType.Date)]
        [Min18YearsIfAMember]
        public Nullable<DateTime> BirthDate { get; set; }
        
        public MembershipTypeDTO MembershipType { get; set; }

        public int MoviesCount { get; set; }
    }
}