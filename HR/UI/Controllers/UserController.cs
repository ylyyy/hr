using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;
using Entity;
using IBLL;
using IocContainer;
using System.Data;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        
        // GET: User
        UserIBLL iab = IocContain.CreateAll<UserIBLL>("yibll", "UserBll");
        RoleIBLL ril = IocContain.CreateAll<RoleIBLL>("yibll","RoleBll");
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[Login]
        //public ActionResult ccc() {
        //    return View();
        //}
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login1(users uu)
        {
           List<users> ss= iab.SelectWhere(e => e.u_name == uu.u_name&&e.u_password==uu.u_password);
            if (uu.u_name.Length >= 6)
            {
                if (ss.Count > 0)
                {
                    users u = ss[0];
                    Session["roid"] = u.u_roleid;
                    Session["user"] = u;
                    return Content("<script>window.location.href='/Main/Index'</script>");
                }
                else
                {
                    return Content("<script>alert('用户名或密码错误！');window.location.href='/User/Login'</script>");
                }
            }
            else
            {
                return Content("<script>alert('用户名必须大于六位！');window.location.href='/User/Login'</script>");
            }
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