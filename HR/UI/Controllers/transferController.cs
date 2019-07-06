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
using DAL;

namespace UI.Controllers
{
    public class transferController : Controller
    {
        HREntities3 aet = new HREntities3();
        transferIBLL tib= IocContain.CreateAll<transferIBLL>("yibll", "transferBll");
        // GET: transfer
        public ActionResult Index()
        {
          return View();
        }
        public ActionResult Index1() {
            List<major_change> user = tib.SelectAll();
            //SelectList userlist = new SelectList(user, "first_kind_id", "first_kind_name");

            //ViewData["select1"] = userlist;

            return Json(user,JsonRequestBehavior.AllowGet);
        }
        public ActionResult mhcx()
        {
             Session["yiji"]= Request["fkid"];
             Session["erji"]= Request["configThird.secondKindId"];
             Session["sanji"]= Request["configThird.thirdKindId"];
            Session["ksdata"]= Request["utilbean.startDate"];
            Session["jsdata"]= Request["utilbean.endDate"];
            //string sql= string.Format(@"select * from major_change  where first_kind_id='{0}' or second_kind_id='{1}' or  third_kind_id='{2}' or (regist_time>='{3}' and check_time <='{4}') and check_status=0", yiji, erji, sanji,ksdata,jsdata);
            //DataTable dt = DBHelper.MyDataAdapter(sql,"");
            return View();
        }
        public ActionResult mhcx2()
        {
            var yiji = Session["yiji"];
            var erji = Session["erji"];
            var sanji = Session["sanji"];
            var ksdata = Session["ksdata"];
            var jsdata = Session["jsdata"];
            string sql = string.Format(@"select * from major_change  where first_kind_id='{0}' or second_kind_id='{1}' or  third_kind_id='{2}' or (regist_time>='{3}' or check_time <='{4}') and check_status=0", yiji, erji, sanji, ksdata, jsdata);
            DataTable dt = DBHelper.MyDataAdapter(sql,"");
            return Content(JsonConvert.SerializeObject(dt));
        }
        public ActionResult Edit(int id) {
            var list=tib.SelectWhere(e=>e.mch_id==id);
            major_change mc = new major_change
            {
                mch_id=list[0].mch_id,
                human_name = list[0].human_name,
                first_kind_name = list[0].first_kind_name,
                second_kind_name = list[0].second_kind_name,
                third_kind_name = list[0].third_kind_name,
                major_kind_name = list[0].major_kind_name,
                major_name=list[0].major_name,
                salary_standard_name=list[0].salary_standard_name,
                salary_sum= list[0].salary_sum,
                register=list[0].register,
                change_reason=list[0].register,
                new_salary_sum=list[0].new_salary_sum
            };


            return View(mc);
        }
       
        public ActionResult Upd()
        {
            var id = Request["mch_id"];
            var yiji = Request["majorChange.newFirstKindId"];
            var erji = Request["majorChange.newSecondKindId"];
            var sanji = Request["majorChange.newThirdKindId"];
            var zwfl = Request["majorChange.newMajorKindId"];
            var zwmc = Request["majorChange.newMajorId"];
            var xcbz = Request["majorChange.newSalaryStandardId"];
            var FText = Request["FText"];
            var SText = Request["SText"];
            var TText = Request["TText"];
            var KText = Request["KText"];
            var MText = Request["MText"];
            var SaText = Request["SaText"];
            string sql = string.Format(@"update major_change set check_status=1,new_first_kind_id='{0}',new_first_kind_name='{1}',new_second_kind_id='{2}',new_second_kind_name='{3}',new_third_kind_id='{4}',new_third_kind_name='{5}',new_major_kind_id='{6}',new_major_kind_name='{7}',new_major_id='{8}',new_major_name='{9}',salary_standard_id='{10}',salary_standard_name='{11}' where mch_id='{12}'", yiji,FText,erji,SText,sanji,TText,zwfl,KText,zwmc,MText,xcbz,SaText,id);
            int num = DBHelper.InsertUpdateDelte(sql);
            if (num > 0)
            {
                return Content("ok");
            }
            else {
                return View();
            }
            
        }
        public ActionResult djcg()
        {
            return View();
        }
     }
    
}