using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using MVCProjectLab.Common;
using System.ComponentModel.DataAnnotations;

namespace MVCProjectLab.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
        [System.Web.Mvc.Compare("Salary")]
        public decimal? ConfirmSalary { get; set; }
        public string EditNameIssue { get; set; }
    }
    public class EmployeeMetaData
    {
        public int ID { get; set; }

        
        [Required(ErrorMessage = "Must Enter {0}")]
        [StringLength(50,MinimumLength =5,ErrorMessage = "Must Enter Less than 50 Letter and Bigger than 5 Latter")]
        [Name("The Name Can't Contain @ or * or be null")]
        //[RegularExpression]
        //[Remote("IsUserNameAvailable","Employees",ErrorMessage ="User Name Already Taken",AdditionalFields = "EditNameIssue")]
        [RemoteClientServer("IsUserNameAvailable", "Employees", ErrorMessage = "User Name Already Taken", AdditionalFields = "EditNameIssue")]      
        public string Name { get; set; }
        
        //  [DisplayFormat(DataFormatString ="{0:d}",ApplyFormatInEditMode =true)] //to display only the date not the time.  in rang you have to add typeof
        [Required(ErrorMessage = "Must Enter {0}")]
        [Range(3000,9000,ErrorMessage ="Must Enter Salary Between {1} and {2}")]
        
        public decimal Salary { get; set; }

    
    }
}