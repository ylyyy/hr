using DAL;
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
    public class machangeController : Controller
    {
        HREntities1 aet = new HREntities1();
        transferIBLL tib = IocContain.CreateAll<transferIBLL>("yibll", "transferBll");
        // GET: machange
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult index2()
        {
            string sql = string.Format(@"select * from major_change where check_status=1");
            DataTable dt = DBHelper.MyDataAdapter(sql, "");
            return Content(JsonConvert.SerializeObject(dt));
        }
        public ActionResult Edit(int id) {
            var list = tib.SelectWhere(e => e.mch_id == id);
            major_change mc = new major_change
            {
                mch_id = list[0].mch_id,
                human_name = list[0].human_name,
                first_kind_name = list[0].first_kind_name,
                second_kind_name = list[0].second_kind_name,
                third_kind_name = list[0].third_kind_name,
                major_kind_name = list[0].major_kind_name,
                major_name = list[0].major_name,
                salary_standard_name = list[0].salary_standard_name,
                salary_sum = list[0].salary_sum,
                register = list[0].register,
                change_reason = list[0].register,
                new_salary_sum = list[0].new_salary_sum
            };
            return View(mc);
        }
        public ActionResult Upd()
        {
            var id = Request["mch_id"];
            var status = Request["majorChange.checkStatus"];
            if (status == "true")
            {
                string sql = string.Format(@"update major_change set check_status=2 where mch_id='{0}'",id);
                int num = DBHelper.InsertUpdateDelte(sql);
                if (num > 0)
                {
                    //return Content("ok");
                    return Redirect("/machange/hscg");
                }
                else {
                    return View();
                }
            }
            else
            {
                string sql = string.Format(@"update major_change set check_status=0 where mch_id='{0}'", id);
                int num = DBHelper.InsertUpdateDelte(sql);
                if (num > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return View();
                }
            }
        }
        public ActionResult hscg() {
            return View();
        }
    }
}