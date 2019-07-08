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
        Ihuman_fileBll mc1 = IocContain.CreateAll<Ihuman_fileBll>("yibll", "human_filebll");
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
and third_kind_id='{2}' and regist_time between '{3}' and '{4}'", Session["first_kind_id"], Session["second_kind_id"], Session["third_kind_id"], Session["regist_time"], Session["check_time"]);
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
        //public ActionResult Indexs()
        //{
        //    string ii = Request["is"];
        //    string sql =string.Format("select salary_sum from human_file where human_major_id='{0}'", ii);
        //    var dt = DBHelper.SelectSinger(sql,"");
        //    return Content(dt.ToString());
        //}
       
        //查询这一列
        public ActionResult Update(short id)
        {
            Session["id"] = id;
            var dt= mc1.SelectWhere(e=>e.huf_id== id);
            Session["dt"]=dt;
            human_file ma = new human_file()
            {
                human_id = dt[0].human_id,
                first_kind_id= dt[0].first_kind_id,
                second_kind_id=dt[0].second_kind_id,
                third_kind_id=dt[0].third_kind_id,
                human_major_kind_id= dt[0].human_major_kind_id,
                human_major_id= dt[0].human_major_id,
                salary_standard_id= dt[0].salary_standard_id,

                human_name = dt[0].human_name,
                first_kind_name = dt[0].first_kind_name,
                second_kind_name = dt[0].second_kind_name,
                third_kind_name = dt[0].third_kind_name,
                human_major_kind_name= dt[0].human_major_kind_name,
                hunma_major_name= dt[0].hunma_major_name,
                //major_kind_name = dt[0].major_kind_name,
                //major_name = dt[0].major_name,
                salary_standard_name =dt[0].salary_standard_name,
                salary_sum=dt[0].salary_sum,
                register=dt[0].register
            };
            return View(ma);
        }
        //修改
        [HttpPost]
        public ActionResult Update()

        {
            var q= Request["newSalaryStandardId"];
            object id = Session["id"];
            string ss = Request["newMajorKindId"];
            short sss = short.Parse(id.ToString());
            var dtq = mc1.SelectWhere(e => e.huf_id == sss);
            human_file ma = new human_file()
            {
                human_id = dtq[0].human_id,
                first_kind_id = dtq[0].first_kind_id,
                second_kind_id = dtq[0].second_kind_id,
                third_kind_id = dtq[0].third_kind_id,
                human_major_kind_id = dtq[0].human_major_kind_id,
                human_major_id = dtq[0].human_major_id,
                salary_standard_id = dtq[0].salary_standard_id,
                human_name = dtq[0].human_name,
                first_kind_name = dtq[0].first_kind_name,
                second_kind_name = dtq[0].second_kind_name,
                third_kind_name = dtq[0].third_kind_name,
                human_major_kind_name = dtq[0].human_major_kind_name,
                
                hunma_major_name = dtq[0].hunma_major_name,
                //major_kind_name = dt[0].major_kind_name,
                //major_name = dt[0].major_name,
                salary_standard_name = dtq[0].salary_standard_name,
                salary_sum = dtq[0].salary_sum,
                register = dtq[0].register
            };
            DateTime dt =Convert.ToDateTime( Request["majorChange.registTime"]);
            string sql = string.Format(@"update dbo.human_file set check_status=2");
//            insert into dbo.major_change
//(first_kind_id, first_kind_name,
//second_kind_id, second_kind_name,
//third_kind_id, third_kind_name,
//human_id, human_name, major_kind_id,
//major_kind_name, major_id, major_name,
//salary_standard_id, salary_standard_name,
//salary_sum, new_first_kind_id, new_first_kind_name,
//new_second_kind_id, new_second_kind_name,
//new_third_kind_id, new_third_kind_name,
//new_major_kind_id, new_major_kind_name, new_major_id,
//new_major_name, new_salary_standard_id,
//new_salary_standard_name, new_salary_sum,
//register, change_reason, regist_time, check_status)
//values('01', '集团', '01', '软件公司', '01', '外包组', 'bt0101010001', 'fantia', '2 ', '软件开发', '1 ', '项目经理', '', '经理级别', '0.0000', '集团', '01', '软件公司', '01', '外包组', '01', '软件开发', '2', '项目经理', '1', '经理级别', '', '1', '1', '1', '2019/7/7 20:26:58', '2')
//                salary_standard_id
//            new_salary_standard_name
            
            string sql1 = string.Format(@"insert into dbo.major_change
(first_kind_id, first_kind_name,
second_kind_id, second_kind_name,
third_kind_id, third_kind_name,
human_id,human_name,major_kind_id,
major_kind_name,major_id,major_name,
salary_standard_id,salary_standard_name,
salary_sum,new_first_kind_id,new_first_kind_name,
new_second_kind_id,new_second_kind_name,
new_third_kind_id,new_third_kind_name,
new_major_kind_id,new_major_kind_name,new_major_id,
new_major_name,new_salary_standard_id,
new_salary_standard_name,new_salary_sum,
register,change_reason,regist_time,check_status)
values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}')", ma.first_kind_id, ma.first_kind_name, ma.second_kind_id, ma.second_kind_name, ma.third_kind_id, ma.third_kind_name, ma.human_id, ma.human_name, ma.human_major_kind_id, ma.human_major_kind_name,ma.human_major_id, ma.hunma_major_name, ma.salary_standard_id,ma.salary_standard_name, ma.salary_sum, Request["firstKindId2"], Request["firstKindId"], Request["secondKindId2"], Request["secondKindId"], Request["thirdKindId2"], Request["thirdKindId"], Request["newMajorKindId2"], Request["newMajorKindId"], Request["newMajorId2"], Request["newMajorId"], Request["newSalaryStandardId2"], Request["newSalaryStandardId"],Request["salary_sum2"],Request["register"],Request["majorChange.changeReason"],dt,2);
           
            int xg=DBHelper.InsertUpdateDelte(sql);
            int zj = DBHelper.InsertUpdateDelte(sql1);
            if (zj > 0)
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