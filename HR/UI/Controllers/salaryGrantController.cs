using Entity;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class salaryGrantController : Controller
    {

        Iconfig_file_first_kindBLL ifirst = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        Iconfig_file_second_kindBLL itwo = IocContain.CreateAll<Iconfig_file_second_kindBLL>("yibll", "config_file_second_kindBLL");
        Iconfig_file_third_kindBLL ithree = IocContain.CreateAll<Iconfig_file_third_kindBLL>("yibll", "config_file_third_kindBLL");
        Ihuman_fileBll human = IocContain.CreateAll<Ihuman_fileBll>("yibll", "humanbll");
        //薪酬发放登记   几级选择
        // GET: salaryGrant
        public ActionResult register_locate()
        {
            return View();
        }
        //薪酬发放登记    几级结构查询
        public ActionResult register_list(string submitType) {
            if (submitType=="1")
            {
                ViewData["jigou"] = 1;
                ViewData["kind"] = ifirst.SelectAll();
            }
            else if (submitType=="2")
            {
                ViewData["jigou"] = 2;
                ViewData["kind"] = itwo.SelectAll();
            }
            else if (submitType=="3")
            {
                ViewData["jigou"] = 3;
                ViewData["kind"] = ithree.SelectAll();
            }
            ViewData["human"] = human.SelectAll();
            return View();
        }
        //薪酬发放登记  登记页面
        public ActionResult register_commit(string jg,string id) {
            ViewData["user"] = "zhangsan";
            if (jg=="1")
            {
                ViewData["jg"] = jg;
                ViewData["firsthm"]=human.SelectWhere(e=>e.first_kind_id==id);
                return View();
            }
            else if (jg=="2")
            {
                ViewData["jg"] = jg;
                ViewData["firsthm"] = human.SelectWhere(e => e.second_kind_id == id);
                return View();
            }
            else if (jg=="3")
            {
                ViewData["jg"] = jg;
                ViewData["firsthm"] = human.SelectWhere(e=>e.third_kind_id==id);
                return View();
            }
            return Content("<script>alert('无页面！');</script>");
        }
    }
}