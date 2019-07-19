using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProjectLab.ViewModels
{
    //Order
        public class OrderViewModel
        {
            public DateTime OrderDate { get; set; }
            public int EmployeeID { get; set; }
            public int CustomerID { get; set; }
            public double Total { get; set; }
            public List<ListOfProduct> ListOfProducts { get; set; }
        }
        public class ListOfProduct
        {
            public int ProductID { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public double TotalPrice { get; set; }
        }
    }