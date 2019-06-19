using Entity;
using IBLL;
using IocContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class clientController : Controller
    {
        ISalaryBll isbll = IocContain.CreateAll<ISalaryBll>("yibll", "sarybll");
        // GET: client
        [HttpGet]
        public ActionResult salary_item()
        {
            List<salary> list = isbll.SelectAll();
            return View(list);
        }
        //薪酬项目名称添加
        [HttpPost]
        public ActionResult salary_item(string itemName)
        {
            salary sary = new salary()
            {
                sid_sname = itemName
            };
            if (isbll.Add(sary) > 0)
            {
                return Content("<script>alert('添加成功！');location.href='/client/salary_item';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！');location.href='/client/salary_item';</script>");
            }
        }
        public ActionResult salary_item_register() {
            return View();
        }
        //薪酬项目名称删除
        public ActionResult Del(int id) {
            salary sary = new salary() {
                sid_id=id
            };
            if (isbll.Delete(sary) > 0)
            {
                return Content("<script>alert('删除成功！');location.href='/client/salary_item';</script>");
            }
            else {
                return Content("<script>alert('删除失败！');location.href='/client/salary_item';</script>");
            }
        }
    }
}