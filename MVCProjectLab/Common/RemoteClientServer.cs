using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace MVCProjectLab.Common
{
    public class RemoteClientServerAttribute : RemoteAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var AddationalFiled = validationContext.ObjectInstance.GetType().GetProperty(this.AdditionalFields);
            var AddationalFiledValue = AddationalFiled.GetValue(validationContext.ObjectInstance, null);
            Type controller = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(type => type.Name.ToLower() == string.Format($"{this.RouteData["controller"].ToString()}Controller").ToLower());
            if (controller != null)
            {
                MethodInfo Action = controller.GetMethods().FirstOrDefault(m => m.Name.ToLower() == this.RouteData["action"].ToString().ToLower());

                if (Action != null)
                {                  
                    object instance = Activator.CreateInstance(controller);
                    dynamic Respone;                                 
                        Respone = Action.Invoke(instance, new object[] { value, AddationalFiledValue});

                    if (Respone is JsonResult)
                    {
                        object jsondata = ((JsonResult)Respone).Data;
                        if (jsondata is bool)
                        {
                            return (bool)jsondata ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
                        }
                    }
                }

            }
            return new ValidationResult("Some thing Programming was wrong you may enter name of method or controller wrong");

        }

        public RemoteClientServerAttribute(string routeName)
           : base(routeName)
        {
        }
       
        public RemoteClientServerAttribute(string action, string controller)
            : base(action, controller)
        {
        }

        public RemoteClientServerAttribute(string action, string controller,
            string areaName) : base(action, controller, areaName)
        {
        }


    }

}