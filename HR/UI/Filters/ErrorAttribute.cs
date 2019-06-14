using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Filters
{
    public class ErrorAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string message = filterContext.Exception.Message;
            filterContext.Controller.ViewData["error"] = message;
            filterContext.Result = new ViewResult {
                ViewName = "Error", ViewData = filterContext.Controller.ViewData//跳转到error页面，把截取到的异常信息给页面中的viewdata
            };
            filterContext.ExceptionHandled = true;//让系统的异常处理程序不处理
        }
    }
}