using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel.DataAnnotations;
namespace MVCProjectLab.Models
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute(string ErrorMessage):base(ErrorMessage)
        {

        }
        public override bool IsValid(object value)
        {
          var dt=  Convert.ToDateTime(value);
            return dt <= DateTime.Now;
        }
    }
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(string Min,string Max):base(typeof(DateTime),Min,DateTime.Now.ToShortDateString())
        {

        } 
       
    }
    public class NameAttribute :ValidationAttribute
    {
        public NameAttribute(string ErrorMessage) : base(ErrorMessage)
        {
            // لو هتستخدم افلدشن البيرجع بى ترو او فلس لزم تغير المسدج بى ده غير كده استخدم التحت ده

        }

        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    //return base.IsValid(value, validationContext);
        //    if (value == null)
        //    {
        //        return new ValidationResult("Must enter name");
        //    }
        //    else if (value.ToString().Contains("@") || value.ToString().Contains("*"))
        //    {
        //        return new ValidationResult("Name must not contain @ or * ");
        //    }

        //    return ValidationResult.Success;
        //}

        public override bool IsValid(object value)
        {
            string Name = Convert.ToString(value);
            return !(Name.Contains("@") || Name.Contains("*") || string.IsNullOrEmpty(Name));
        }

    }

}