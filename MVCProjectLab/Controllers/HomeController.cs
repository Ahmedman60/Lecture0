using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectLab.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }
        //this method will not public in the UrL you can make it using also idenitity

        [NonAction]
        public ActionResult Contact()
        {
            return View();
        }
    }
}