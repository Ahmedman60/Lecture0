using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using System.Collections;
using MVCProjectLab.Models;
using MVCProjectLab.ViewModels;


namespace MVCProjectLab.Controllers
{
    public class OrdersController : Controller
    {
        private MVCProjectsDBEntities db = new MVCProjectsDBEntities();
        
        public JsonResult GetPrice(string ProductID)
        {
            
          
           
            db.Configuration.ProxyCreationEnabled = false;
            int Pid = int.Parse(ProductID);
            var p = db.Products.Find(Pid);

            return Json(p.Price, JsonRequestBehavior.AllowGet);
        }
        // GET: Orders
        [HttpGet]
        public ActionResult AddOrder()
        { 
            // new selectedlist  and go change the type of viewbag to selectedlist as it's d
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName");


            //list or any ienuramble of selectedlistitem   شروط فى الDropdownlist
            //List<SelectListItem> selectLists = new List<SelectListItem>(); 
            //foreach (var item in db.Employees)
            //{
            //    SelectListItem i = new SelectListItem()
            //    {
            //        Text = item.Salary>5000? "VIP"+item.Name:item.Name, //without trid operation you can't 
            //        //make if here
            //        Value = item.ID.ToString(),
                    
                    
               
            //    };
            //    selectLists.Add(i);
            //}
            //ViewBag.Employee2 = selectLists;
            return View();
        }
        [HttpPost]
        public JsonResult AddOrder(OrderViewModel orderviewmodel)
        {
            
            bool status = true;
            var isvalidModel = TryUpdateModel(orderviewmodel);
            if (isvalidModel)
            {
                using (MVCProjectsDBEntities db = new MVCProjectsDBEntities())
                {
                    Order o = new Order()
                    {
                        OrderDate = orderviewmodel.OrderDate,
                        EmployeeID = orderviewmodel.EmployeeID,
                        CustomerID = orderviewmodel.CustomerID,
                        Total =Convert.ToDecimal( orderviewmodel.Total)
                    };
                    db.Orders.Add(o);
                    if (db.SaveChanges() > 0)
                    {

                        int orderId =  db.Orders.Max(q => q.ID); //o.ID;
                        foreach (var item in orderviewmodel.ListOfProducts)
                        {
                            OrderDetail od = new OrderDetail()
                            {
                                OrderID = orderId,
                                ProductID = item.ProductID,
                                Price = Convert.ToDecimal(item.Price),
                                Quantity = item.Quantity,
                                Total = Convert.ToDecimal(item.TotalPrice)
                            };
                            db.OrderDetails.Add(od);
                            // db.SaveChanges();
                        }
                        if (db.SaveChanges() > 0)
                        {
                            return new JsonResult { Data = new { status = status, message = "Saving is Done" } };
                        }
                    }
                }
            }
            status = false;
            return new JsonResult { Data = new { status = status, message = "Saving not Add !!" } };
        }
    }
}