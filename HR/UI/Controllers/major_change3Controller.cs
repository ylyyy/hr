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
    public class major_change3Controller : Controller
    {

        major_changeIBLL mc = IocContain.CreateAll<major_changeIBLL>("yibll", "major_changeBll");

        // GET: major_change3
        public ActionResult Index()
        {
            //Session["first_kind_id"]
            //   string s = Request.Form["majorChange.newFirstKindId"];
            //Session["second_kind_id"] = Request["configThird.secondKindId"];
            //Session["third_kind_id"] = Request["configThird.thirdKindId"];
            //Session["regist_time"] = Request["utilbean.startDate"];
            //Session["check_time"] = Request["utilbean.endDate"];
            return View();
        }
        //查询全部
        public ActionResult Index2()
        {
            string where = "";
            string s= Request["majorChange.newFirstKindId"];
            if (s.Equals("0"))
            {
                where = "select * from major_change where check_status=3";
            }
            if (!s.Equals("0"))
            {
                where =string.Format("select * from major_change where check_status=3 and first_kind_id='{0}'",s);
            }
            Session["sql"] = where;

            return View();
        }

        public ActionResult Index3()
        {
            string sql = Session["sql"].ToString();
            var dt = DBHelper.Select(sql, "");
            return Content(JsonConvert.SerializeObject(dt));
        }

        //查看
        public ActionResult Update(short id)
        {
            var dt = mc.SelectWhere(e => e.mch_id == id);
            major_change ma = new major_change()
            {
                human_id = dt[0].human_id,
                human_name = dt[0].human_name,
                first_kind_name = dt[0].first_kind_name,
                second_kind_name = dt[0].second_kind_name,
                third_kind_name = dt[0].third_kind_name,
                major_kind_name = dt[0].major_kind_name,
                major_name = dt[0].major_name,
                salary_standard_name = dt[0].salary_standard_name,
                salary_sum = dt[0].salary_sum,
                register = dt[0].register,
                regist_time = dt[0].regist_time,
                checker = dt[0].checker,
                check_time = dt[0].check_time,
                change_reason = dt[0].change_reason,
                check_reason = dt[0].check_reason,
                new_salary_sum = dt[0].new_salary_sum,
            };
            GetList();
            GetList1();
            GetList2();
            GetList3();
            GetList4();
            GetList5();
            return View(ma);
        }

     

        //下拉列表
        public void GetList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string sql = "select new_first_kind_id,new_first_kind_name from major_change";
            var dt = DBHelper.Select(sql, "");
            foreach (DataRow item in dt.Rows)
            {
                SelectListItem st = new SelectListItem
                {
                    Value = item["new_first_kind_id"].ToString(),
                    Text = item["new_first_kind_name"].ToString()
                };
                list.Add(st);
            }
            ViewData["s"] = list;
        }
        //下拉列表
        public void GetList1()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string sql = "select new_second_kind_id,new_second_kind_name  from major_change";
            var dt = DBHelper.Select(sql, "");
            foreach (DataRow item in dt.Rows)
            {
                SelectListItem st = new SelectListItem
                {
                    Value = item["new_second_kind_id"].ToString(),
                    Text = item["new_second_kind_name"].ToString()
                };
                list.Add(st);
            }
            ViewData["s1"] = list;
        }
        //下拉列表
        public void GetList2()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string sql = "select new_third_kind_id,new_third_kind_name from major_change";
            var dt = DBHelper.Select(sql, "");
            foreach (DataRow item in dt.Rows)
            {
                SelectListItem st = new SelectListItem
                {
                    Value = item["new_third_kind_id"].ToString(),
                    Text = item["new_third_kind_name"].ToString()
                };
                list.Add(st);
            }
            ViewData["s2"] = list;
        }
        //下拉列表
        public void GetList3()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string sql = "select new_major_kind_id,new_major_kind_name from major_change";
            var dt = DBHelper.Select(sql, "");
            foreach (DataRow item in dt.Rows)
            {
                SelectListItem st = new SelectListItem
                {
                    Value = item["new_major_kind_id"].ToString(),
                    Text = item["new_major_kind_name"].ToString()
                };
                list.Add(st);
            }
            ViewData["s3"] = list;
        }
        //下拉列表
        public void GetList4()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string sql = "select new_major_id,new_major_name from major_change";
            var dt = DBHelper.Select(sql, "");
            foreach (DataRow item in dt.Rows)
            {
                SelectListItem st = new SelectListItem
                {
                    Value = item["new_major_id"].ToString(),
                    Text = item["new_major_name"].ToString()
                };
                list.Add(st);
            }
            ViewData["s4"] = list;
        }
        //下拉列表
        public void GetList5()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string sql = "select new_salary_standard_id,new_salary_standard_name from major_change";
            var dt = DBHelper.Select(sql, "");
            foreach (DataRow item in dt.Rows)
            {
                SelectListItem st = new SelectListItem
                {
                    Value = item["new_salary_standard_id"].ToString(),
                    Text = item["new_salary_standard_name"].ToString()
                };
                list.Add(st);
            }
            ViewData["s5"] = list;
        }
    }
}