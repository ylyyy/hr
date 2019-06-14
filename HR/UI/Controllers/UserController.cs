using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    
    public class UserController : Controller
    {
        
        // GET: User
        
        public ActionResult Index()
        {
            return View();
        }
        [Login]
        public ActionResult ccc() {
            return View();
        }
        //public ActionResult Error() {
        //    throw new Exception("报错了！！");
        //}
    }
}