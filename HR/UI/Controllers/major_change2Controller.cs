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
    public class major_change2Controller : Controller
    {
        int page = 0;
        int rows = 0;
        int currentPage = 0;
        string where = "where check_status=1";
        HREntities1 aet = new HREntities1();
        major_changeIBLL mc = IocContain.CreateAll<major_changeIBLL>("yibll", "major_changeBll");
        
        // GET: major_change2
        public ActionResult Index()
        {
            return View();
        }
        //分页查询
        public ActionResult Index2()
        {
            var dt = mc.FenYe2(currentPage, ref page, ref rows, where);
            Dictionary<string, object> dr = new Dictionary<string, object>();
            dr["dt"] = dt;
            dr["page"] = page;
            dr["rows"] = rows;
            return Content(JsonConvert.SerializeObject(dr));
        }
        //查询单行
        public ActionResult Update(short id)
        {
            Session["ids"] = id;
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
                check_reason = dt[0].check_reason
            };
            GetList();
            GetList1();
            GetList2();
            GetList3();
            GetList4();
            GetList5();
            return View(ma);
        }

        //修改
        [HttpPost]
        public ActionResult Update(major_change mc)
        {
           string s= Request["s"];
            string sql = "";
            if (s=="true")
            {
                sql =string.Format(@" update dbo.major_change set check_status=3,check_reason='{0}'
    where mch_id = '{1}'",mc.check_reason,Session["ids"]);
            }
            else
            {
                sql = string.Format(@" update dbo.major_change set check_status=1
    where mch_id = '{0}'", Session["ids"]);
            }

            int xg=DBHelper.InsertUpdateDelte(sql);
            if (xg>0)
            {
                return Content("ok");
            }
            else
            {
                return Content("ok");
            }
        }

        public ActionResult Index3()
        {
            return View();
        }

        //下拉列表
        public void GetList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            string sql = "select new_first_kind_id,new_first_kind_name from major_change";
            var dt = DBHelper.Select(sql,"");
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