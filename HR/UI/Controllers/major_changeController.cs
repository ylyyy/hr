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
    public class major_changeController : Controller
    {
        major_changeIBLL mc = IocContain.CreateAll<major_changeIBLL>("yibll", "major_changeBll");
        
        int page = 0;
        int rows = 0;
        string where = "";  
        // GET: major_change
        //查询全部
        public ActionResult Index()
        {
            Session["first_kind_id"] = Request["configThird.firstKindId"];
            Session["second_kind_id"] = Request["configThird.secondKindId"];
            Session["third_kind_id"] = Request["configThird.thirdKindId"];
            Session["regist_time"] = Request["utilbean.startDate"];
            Session["check_time"] = Request["utilbean.endDate"];
            try
            {
                if (Request["configThird.firstKindId"].Equals("0") && Request["utilbean.startDate"] == "")
                {

                    where = string.Format(@"where 1=1 and check_status=1");
                }
                if (!Request["configThird.thirdKindId"].Equals("0") && Request["utilbean.startDate"] != "")
                {
                    where = string.Format(@"where check_status=1 and  first_kind_id='{0}' and second_kind_id='{1}' 
and third_kind_id='{2}' and reate_time between '{3}' and '{4}'", Session["first_kind_id"], Session["second_kind_id"], Session["third_kind_id"], Session["regist_time"], Session["check_time"]);
                }
            }
            catch (Exception)
            {
                where = string.Format(@"where 1=1 and check_status=1");
            }
           
            Session["where"] = where;
            return View();
        }

        //分页查询
        public ActionResult Index2()
        {
            int currentPage = Convert.ToInt32(Request["currentPage"]);
            where = string.Format(@"where first_kind_id='{0}' and second_kind_id='{1}' 
and third_kind_id='{2}' and regist_time>='{3}' and check_time<='{4}'", Session["first_kind_id"], Session["second_kind_id"], Session["third_kind_id"], Session["regist_time"], Session["check_time"]);
            var dt = mc.FenYe(currentPage, ref page, ref rows, Session["where"].ToString());
            Dictionary<string, object> dr = new Dictionary<string, object>();
            dr["dt"] = dt;
            dr["page"] = page;
            dr["rows"] = rows;
            return Content(JsonConvert.SerializeObject(dr));
        }

        //选择总金额
        public ActionResult Indexs()
        {
            string ii = Request["is"];
            string sql =string.Format("select salary_sum from human_file where human_major_id='{0}'", ii);
            var dt = DBHelper.SelectSinger(sql,"");
            return Content(dt.ToString());
        }

        //查询这一列
        public ActionResult Update(short id)
        {
            Session["id"] = id;
            var dt=mc.SelectWhere(e=>e.mch_id==id);
            major_change ma = new major_change()
            {
                human_id = dt[0].human_id,
                human_name = dt[0].human_name,
                first_kind_name = dt[0].first_kind_name,
                second_kind_name = dt[0].second_kind_name,
                third_kind_name = dt[0].third_kind_name,
                major_kind_name = dt[0].major_kind_name,
                major_name = dt[0].major_name,
                salary_standard_name=dt[0].salary_standard_name,
                salary_sum=dt[0].salary_sum,
                register=dt[0].register
            };
            return View(ma);
        }
        //修改
        [HttpPost]
        public ActionResult Update()
        {
            object id = Session["id"];
            string ss = Request["newMajorKindId"];

            DateTime dt =Convert.ToDateTime( Request["majorChange.registTime"]);
            string sql = string.Format(@"update  dbo.major_change set check_status=2,new_first_kind_name='{0}',new_second_kind_name='{1}',new_third_kind_name='{2}',
    new_major_kind_name = '{3}',new_major_name = '{4}',salary_standard_name = '{5}',salary_sum = '{6}',register = '{7}',regist_time = '{8}',change_reason = '{9}',new_first_kind_id ='{11}',
    new_second_kind_id ='{12}',new_third_kind_id='{13}',new_major_kind_id='{14}',new_major_id='{15}',new_salary_standard_id='{16}'
    where mch_id = '{10}'", Request["firstKindId"], Request["secondKindId"], Request["thirdKindId"], Request["newMajorKindId"], Request["newMajorId"], Request["newSalaryStandardId"],
     Request["salary_sum2"], Request["register"], dt, Request["majorChange.changeReason"],id,Request["firstKindId2"], Request["secondKindId2"], Request["thirdKindId2"], Request["newMajorKindId2"], Request["newMajorId2"], Request["newSalaryStandardId2"]);
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
    }
}