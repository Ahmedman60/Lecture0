using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel.DataAnnotations;

namespace MVCProjectLab.Models
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {

    }
    public class CustomerMetaData
    {
        public int ID { get; set; }

        [Required (ErrorMessage ="Must Enter {0}")]
        [StringLength(50 ,ErrorMessage ="Must Enter Less than 50 Letter")]
        public string Name { get; set; }
    }
}