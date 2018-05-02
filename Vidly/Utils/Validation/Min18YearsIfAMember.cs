using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Utils.Validation
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = validationContext.ObjectInstance as Customer;

            if(customer == null)
                return ValidationResult.Success;

            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if(customer.BirthDate == null)
                return new ValidationResult("Birthdate is required");

            var now = DateTime.UtcNow;
            var age = new DateTime(now.Subtract(customer.BirthDate.Value).Ticks).Year - 1; 
            if(age >= 18)
                return ValidationResult.Success;

            return new ValidationResult("Customer should be at least 18 years old to be a member");

        }
    }
}