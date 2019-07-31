using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProjectLab.Models;
using PagedList;
using PagedList.Mvc;
namespace MVCProjectLab.Controllers
{
    public class EmployeesController : Controller
    {
        private MVCProjectsDBEntities db = new MVCProjectsDBEntities();
        
        // GET: Employees
        //[HttpGet]
        //public ActionResult Index(int? page)
        //{
        //    var employees = db.Employees.Include(e => e.Department).ToList();
        //    return View(employees.ToPagedList(page??1,3));
        //}

        [OutputCache(CacheProfile = "10SecondCache")]
        public ActionResult Index(string SearchBy, string search, int? page, string sortby)
        {
            
            //sorting by
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortby) ? "Name desc" : "";
            ViewBag.SortSalaryParameter = sortby == "Salary" ? "Salary desc" : "Salary";
            //because Querystring doesn't work
            if (search != null)
            {
                TempData["HoldSearch"] = search;
                TempData["HoldSearch2"] = SearchBy;
                TempData.Keep();
            }
            else
            {
                search = (string)TempData["HoldSearch"];
                SearchBy = (string)TempData["HoldSearch2"];
                TempData.Keep();
            }

            var Emp = db.Employees.AsQueryable();
            //63
            if (SearchBy == "Department")
            {
                Emp = db.Employees.Where(e => e.Department.Name == search || search == null);
            }
            else if (SearchBy == "Name")
            {                //Name search
                Emp = db.Employees.Where(e => e.Name.StartsWith(search) || search == null);

            }
            else
            {
                Emp = db.Employees.Include(e => e.Department);

            }

            switch (sortby)
            {
                case "Name desc":
                    Emp = Emp.OrderByDescending(x => x.Name);
                    break;

                case "Salary desc":
                    Emp = Emp.OrderByDescending(x => x.Salary);
                    break;
                case "Salary":
                    Emp = Emp.OrderBy(x => x.Salary);
                    break;
                default:
                    Emp = Emp.OrderBy(x => x.Name);
                    break;

            }
            return View(Emp.ToPagedList(page ?? 1, 2));


        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Salary,DepartmentID,ConfirmSalary,EditNameIssue")] Employee employee)
        {
            //if(db.Employees.Any(e => e.Name == employee.Name))
            //{
            //    //the Key is the Properity
            //    ModelState.AddModelError("Name", "The Name Is Already Exist and turn on the Javasctipt");
            //    //this will turn IsValid Properity to false.      
            //}
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }
        //Added Method Feto.
        public JsonResult IsUserNameAvailable(string Name, string EditNameIssue)
        {//it will return true if match found elese it will return false. so i add !
            //Edit Request  
            
            if (Name == EditNameIssue)
            {
                //this mean he didn't change the name
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if (Name != EditNameIssue)
            {
                //if he change the name in the edit go and check if the new name exist 
                //note if he modify and reenter it origin name it will be also erro he has to reload
                return Json(!db.Employees.Any(e => e.Name == Name), JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrEmpty(EditNameIssue))
            {//this mean you came from create request as there is no EditNameIssue in this view

                return Json(!db.Employees.Any(e => e.Name == Name), JsonRequestBehavior.AllowGet);

            }
            else
            {//just for the completeness
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }


        // GET: Employees/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);

            employee.ConfirmSalary = employee.Salary;
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Salary,DepartmentID,ConfirmSalary,EditNameIssue")] Employee employee)
        {
            //if (db.Employees.Any(e => e.Name == employee.Name))
            //{//this cause violation to the sepration of concerns all the validation should be in model.
            //    //the model is the one who responisble for checking if the model data is valid or not 
            //    //not the controller so try to customize remote attribute.
            //    //the Key is the Properity
            //    ModelState.AddModelError("Name", "The Name Is Already Exist and turn on the Javasctipt");
            //    //this will turn IsValid Properity to false.      
            //}
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
