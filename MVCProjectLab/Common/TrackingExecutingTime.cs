using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVCProjectLab.Common
{
    public class TrackingExecutingTime :ActionFilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
         
        }

        private void LoggingMessage(string Data)
        {
            System.IO.File.AppendAllText("", Data);
        }
    }
}