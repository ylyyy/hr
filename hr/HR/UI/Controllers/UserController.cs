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
        public ActionResult Login() {
            return Content("<script>alert('登录成功！');location.href='/Index/Index';</script>");
        }
        //[Login]
        //public ActionResult ccc() {
        //    return View();
        //}
        //public ActionResult Error() {
        //    throw new Exception("报错了！！");
        //}
    }
}