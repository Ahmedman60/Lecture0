using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel.DataAnnotations;
namespace MVCProjectLab.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {

    }
    public class ProductMetaData
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Must Enter {0}")]
        [StringLength(50, ErrorMessage = "Must Enter Less than 50 Letter")]
        public string ProductName { get; set; }
        [Range(1, 1000, ErrorMessage = "Must Enter {0} Between {1} and {2}")]
        public Nullable<decimal> Price { get; set; }
        public string Photo { get; set; }
        //[FileExtensions(Extensions ="JPG,PNG")]
        public HttpPostedFileBase FilePhoto { get; set; }
    }
}