using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProjectLab.Models;
//
using System.IO;
using System.Threading;

namespace MVCProjectLab.Controllers
{
    public class ProductsController : Controller
    {

        #region CRUD  
        private MVCProjectsDBEntities db = new MVCProjectsDBEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Error400s", "CustomError");
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                //return HttpNotFound();
                return RedirectToAction("Error404s", "CustomError");
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            //Save in Object to save into DB
            string _fileName = "";//Path.GetFileNameWithoutExtension(product.FilePhoto.FileName);
            string _Extenstion = Path.GetExtension(product.FilePhoto.FileName);
            _fileName = product.ProductName + DateTime.Now.ToString("yyMMddhhssfff") + _Extenstion;
            //store in DB the FileName
            product.Photo = _fileName;
            //To Upload in Folder Upload 
            _fileName = Path.Combine(Server.MapPath("~/Upload/Images/") + _fileName);
            
            //save the photo  in the filephoto into the upload file
            product.FilePhoto.SaveAs(_fileName);
            //------------------------------

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Error400s", "CustomError");
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                //return HttpNotFound();
                return RedirectToAction("Error404s", "CustomError");
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            Product OldProduct = db.Products.Find(product.ID);
            if (product.FilePhoto != null)
            {
                //Save in Object to save into DB
                string _fileName = Path.GetFileNameWithoutExtension(product.FilePhoto.FileName);
                string _Extenstion = Path.GetExtension(product.FilePhoto.FileName);
                _fileName = OldProduct.Photo;
                product.Photo = _fileName;
                //To Upload in Folder Upload 
                _fileName = Path.Combine(Server.MapPath("~/Upload/Images/") + _fileName);
                product.FilePhoto.SaveAs(_fileName);
            }
            //------------------------------

            if (ModelState.IsValid)
            {
                OldProduct.ProductName = product.ProductName;
                OldProduct.Price = product.Price;
                //db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Error400s", "CustomError");
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                //return HttpNotFound();
                return RedirectToAction("Error404s", "CustomError");
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            string path = Path.Combine(Server.MapPath("~/Upload/Images/") + product.Photo);
            System.IO.File.Delete(path);
            db.Products.Remove(product);
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

        #endregion
        //[HandleError(View = "_NotFoundImage")]
        public ActionResult MyAction(string path)
        {
            try
            {
                    path = Path.Combine(Server.MapPath("~/Upload/Images/") + path);
                    byte[] imgarray = System.IO.File.ReadAllBytes(path);
                    return new FileContentResult(imgarray, "image/jpg");
            }
            catch (Exception)
            {
                return View("_NotFoundImage");
            }
          
            
        }
        [HttpPost]
        public ActionResult Search(string ProductName)
        {
            var SearchProduct = db.Products.Where(q => q.ProductName.ToLower().StartsWith(ProductName));
            return View("Index", SearchProduct.ToList());
        }

        public PartialViewResult AjaxSearch(string ProductNameAjax)
        {
            Thread.Sleep(3000);
            var Products = db.Products.Where(p => p.ProductName.ToLower().StartsWith(ProductNameAjax));
            return PartialView("_GridResultProduct", Products.ToList());
        }

        public PartialViewResult LowProductPrice()
        {
          
            var LowerProduct = db.Products.OrderBy(p => p.Price).First();
            return PartialView("_ProductLowPrice", LowerProduct);
        }

    }
}
