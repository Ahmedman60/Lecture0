using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel.DataAnnotations;
namespace MVCProjectLab.Models
{
    public class NameAttribute :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);
            if (value == null)
            {
                return new ValidationResult("Must enter name");
            }
            else if (value.ToString().Contains("@") || value.ToString().Contains("*"))
            {
                return new ValidationResult("Name must not contain @ or * ");
            }

            return ValidationResult.Success;
        }
    }
}