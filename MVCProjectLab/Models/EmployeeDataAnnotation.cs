using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel.DataAnnotations;

namespace MVCProjectLab.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {

    }
    public class EmployeeMetaData
    {
        public int ID { get; set; }

       
        [Required(ErrorMessage = "Must Enter {0}")]
        [StringLength(50, ErrorMessage = "Must Enter Less than 50 Letter")]
        [Name]
        //[RegularExpression]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must Enter {0}")]
        [Range(3000,9000,ErrorMessage ="Must Enter Salary Between {1} and {2}")]
        public decimal Salary { get; set; }
    }
}