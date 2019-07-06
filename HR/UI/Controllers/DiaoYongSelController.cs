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
    public class DiaoYongSelController : Controller
    {
        HREntities1 aet = new HREntities1();
        transferIBLL tib = IocContain.CreateAll<transferIBLL>("yibll", "transferBll");
        // GET: DiaoYongSel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelChange()
        {
            Session["yiji"] = Request["firstKindId"];
            Session["erji"] = Request["configThird.secondKindId"];
            Session["sanji"] = Request["configThird.thirdKindId"];
            Session["ksdata"] = Request["utilbean.startDate"];
            Session["jsdata"] = Request["utilbean.endDate"];
            return View();
        }
        public ActionResult SelChange2()
        {
            var yiji = Session["yiji"];
            var erji = Session["erji"];
            var sanji = Session["sanji"];
            var ksdata = Session["ksdata"];
            var jsdata = Session["jsdata"];
            string sql = string.Format(@"select * from major_change  where first_kind_id='{0}' or second_kind_id='{1}' or  third_kind_id='{2}' or (regist_time>='{3}' or check_time <='{4}') and check_status=2", yiji, erji, sanji, ksdata, jsdata);
            DataTable dt = DBHelper.MyDataAdapter(sql, "");
            return Content(JsonConvert.SerializeObject(dt));
        }
        public ActionResult Sel(int id) {
            var list = tib.SelectWhere(e => e.mch_id == id);
            major_change mc = new major_change
            {
                regist_time=list[0].regist_time,
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
                new_salary_sum = list[0].new_salary_sum,
                new_first_kind_name= list[0].new_first_kind_name,
                new_second_kind_name= list[0].new_second_kind_name,
                new_third_kind_name= list[0].new_second_kind_name,
                new_major_kind_name= list[0].new_major_kind_name,
                new_major_name= list[0].new_major_name,
                checker= list[0].checker
            };
            return View(mc);
        }
    }
}