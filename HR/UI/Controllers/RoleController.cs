using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using IBLL;
using IocContainer;
using System.Data;
using Newtonsoft.Json;

namespace UI.Controllers
{
    public class RoleController : Controller
    {
        UserIBLL iab = IocContain.CreateAll<UserIBLL>("yibll", "UserBll");
        HREntities3 aet = new HREntities3();
        RoleIBLL ril = IocContain.CreateAll<RoleIBLL>("yibll", "RoleBll");
        // GET: Role
        public ActionResult Index()
        {
            DataTable da = iab.QuanUserSel();
            ViewData["da"] = GetList();
            return View();

        }
        public ActionResult Index2(/*int currentPage, int pageSize*/)
        {
            //int rows=0;
            //List<users> list = iab.FenYestandard(e =>e.u_id,e=>e.u_id> 0,out rows, currentPage, pageSize);
            //Dictionary<string, object> msg1= new Dictionary<string, object>();
            //msg1["dt"] = list;
            //msg1["row"] = rows;

            //return Content(JsonConvert.SerializeObject(msg1));
            DataTable dt = iab.QuanUserSel();
            return Content(JsonConvert.SerializeObject(dt));
        }
        public ActionResult Create()
        {
            ViewData["dt"] = GetList();
            return View();
        }
        public List<Role> GetList()
        {
            var dt = ril.SelectAll();

            return dt;
        }

        [HttpPost]
        public ActionResult Create(users u)
        {
            int num = iab.Add(u);
            if (num > 0)
            {
                return Content("ok");
            }
            else
            {
                return View();
            }
        }
        public void sss()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //var dt = from a in aet.Role
            //         join s in aet.users on a.roleid equals s.u_roleid
            //         select new { a.rolename, s.u_roleid };
            var dt = GetList();
            foreach (var dr in dt)
            {
                SelectListItem c = new SelectListItem()
                {
                    Text = dr.rolename.ToString(),
                    Value = dr.roleid.ToString()
                };
                list.Add(c);
            }
            Session["s"] = list;
        }
        public ActionResult Edit(int id)
        {
            sss();
            var s = iab.SelectWhere(e => e.u_id == id);
            users u = new users
            {
                u_id = s[0].u_id,
                u_name = s[0].u_name,
                u_password = s[0].u_password,
                u_true_name = s[0].u_true_name,
                u_roleid = s[0].u_roleid
            };
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(users u)
        {
            int num = iab.Update(u);
            if (num>0)
            {
                return Content("ok");
            }
            else { return View(); }
        }
        public ActionResult Delete(int id) {
            users u = new users();
            u.u_id = id;
            if (iab.Delete(u) > 0)
            {
                return Content("<script>alert('删除成功！');window.location.href='/Role/Index'</script>");
            }
            else {
                return Content("<script>alert('删除失败！');window.location.href='/Role/Index'</script>");
            }
        }

    }
}