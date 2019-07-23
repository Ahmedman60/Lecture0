using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectLab.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncUpload(HttpPostedFileBase  file)
        {
            int count = 0;
            if(file != null)
            {
                //foreach (var item in files)
                //{
                //    if(item!=null && item.ContentLength > 0)
                //    {
                //        var filename = Guid.NewGuid() + Path.GetExtension(item.FileName);
                //        var path = Path.Combine(Server.MapPath("~/Upload"), filename);
                //        item.SaveAs(path);
                //        count++;
                //    }
                    //Logic
              //  }
                return new JsonResult { Data = "Successfully " + count + " Files Uploaded" };
            }
            else
            {
                return new JsonResult { Data = "No Files " + count + " Was  Uploaded" };
            }
           
            
        }
    }
}