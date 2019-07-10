using Entity;
using IBLL;
using IocContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using System.Linq.Expressions;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class salaryCriterionController : Controller                                         
    {
        Isalary_standard_detailsBll isd = IocContain.CreateAll<Isalary_standard_detailsBll>("yibll", "ssdbll");
        ISalaryBll isbll = IocContain.CreateAll<ISalaryBll>("yibll", "sarybll");
        ISalary_strandardBll issbll = IocContain.CreateAll<ISalary_strandardBll>("yibll", "ssarybll");
        IusersBll iuser = IocContain.CreateAll<IusersBll>("yibll", "userbll");
        Ihuman_fileBll human = IocContain.CreateAll<Ihuman_fileBll>("yibll", "humanbll");
        // GET: salaryCriterion
        //薪酬标准登记
        public ActionResult salarystandard_register()
        {
            ViewData["user"] = "zhangsan";
            ViewData["salary_bhao"]=isd.bianHao();
            List<salary> list= isbll.SelectAll();
            ViewData["list"] = list;
            ViewData["users"] = iuser.SelectAll();
            return View();
        }
        //添加成功跳转页面
        public ActionResult salarystandard_register_success_cg() {
            return View();
        }
        //查询要添加的salary
        public ActionResult salarystandard_register_success_salary(string salaryid) {
            Array ary = salaryid.Split(',');
            List<salary> salaryst = new List<salary>();
            for (int i = 0; i < ary.Length; i++)
            {
                int sid = Convert.ToInt32(ary.GetValue(i));
                salaryst.Add(isbll.SelectWhere(e => e.sid_id == sid).FirstOrDefault());

            }
            return Content(JsonConvert.SerializeObject(salaryst));
        }
        //薪酬标准添加
        public ActionResult salarystandard_register_success() {
            //获取ui传过来的数据，添加薪酬标准基本信息表成功后再 一行一行添加详情 ，如果出错则ts事务不提交
            using (TransactionScope ts = new TransactionScope())
            {
                string standard_id = Request.Form["standard_id"];
                string standard_name = Request.Form["standard_name"];
                string designer = Request.Form["designer"];
                string register = Request.Form["register"];
                string regist_time = Request.Form["regist_time"];
                string salary_sum = Request.Form["salary_sum"];
                string remark = Request.Form["remark"];
                Array salaryid = Request.Form["salaryid"].Split(',');
                Array details = Request.Form["details"].Split(',');
                Array salarymoney = Request.Form["salarymoney"].Split(',');
                salary_standard sd = new salary_standard()
                {
                    standard_id = standard_id,
                    standard_name = standard_name,
                    designer = designer,
                    register = register,
                    regist_time = Convert.ToDateTime(regist_time),
                    salary_sum = Convert.ToDecimal(salary_sum),
                    remark = remark,
                    change_status = 1,
                    check_status=0
                };
                bool bol = true;
                if (issbll.Add(sd) > 0)
                {
                    for (int i = 0; i < salaryid.Length; i++)
                    {
                        int sh = Convert.ToInt32(salaryid.GetValue(i));
                        salary_standard_details ssd = new salary_standard_details()
                        {
                            standard_id = standard_id,
                            standard_name = standard_name,
                            item_id = (short)(sh),
                            item_name = details.GetValue(i).ToString(),
                            salary = Convert.ToDecimal(salarymoney.GetValue(i))
                        };
                        if (isd.Add(ssd) > 0)
                        {
                            bol = true;
                        }
                        else
                        {
                            bol = false;
                            break;
                        }
                    }
                }
                else
                {
                    bol = false;
                }
                if (bol)
                {
                    ts.Complete();
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            };
        }
        //薪酬标准登记复核
        [HttpGet]
        public ActionResult salarystandard_check_list()
        {
            return View();
        }
        //复核分页
        private string getList(Expression<Func<salary_standard,bool>> where,int currentPage)
        {
            int rows = 0;
            List<salary_standard> list = issbll.FenYe(where, out rows,currentPage, 10);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            int pages = rows % 10 == 0 ? rows / 10 : rows / 10 + 1;
            dic.Add("rows", rows);
            dic.Add("pages", pages);
            dic.Add("currentpage", currentPage);
            dic.Add("list", list);
            return JsonConvert.SerializeObject(dic);
        }
        
        [HttpPost]
        public ActionResult salarystandard_check_list(int currentPage)
        {
            return Content(getList(e=>e.check_status==0,currentPage));
        }
        //复核查询
        public ActionResult salarystandard_check(string id) {
            List<salary_standard_details> list= isd.SelectWhere(e=>e.standard_id==id);
            ViewData["alary_details"] = list;
            List<salary_standard> sary_stan = issbll.SelectWhere(e=>e.standard_id==id);
            ViewData["standard"] = sary_stan;
            ViewData["users"] = iuser.SelectAll();
            return View();
        }

        [HttpGet]
        public ActionResult salarystandard_check_success() {
            return View();
        }
        //复核修改
        [HttpPost]
        public ActionResult salarystandard_check_success(string standard_id,string fhr,string fhtime,string fhyj)
        {
            using (TransactionScope ts=new TransactionScope())
            {
                salary_standard sary = issbll.SelectWhere(e => e.standard_id == standard_id)[0];
                sary.checker = fhr;
                sary.check_time = Convert.ToDateTime(fhtime);
                sary.check_comment = fhyj;
                sary.check_status = 1;
                sary.change_status = 2;

                bool bol = true;
                if (issbll.Update(sary) > 0)
                {
                    List<human_file> hm = human.SelectWhere(e => e.salary_standard_id == standard_id);
                    salary_standard stan = issbll.SelectWhere(e => e.standard_id == standard_id).FirstOrDefault();
                    for (int i = 0; i < hm.Count; i++)
                    {
                        hm[i].salary_sum = stan.salary_sum;
                        hm[i].demand_salaray_sum = stan.salary_sum;
                        hm[i].paid_salary_sum = stan.salary_sum - (stan.salary_sum * Convert.ToDecimal(0.05));
                        if (human.Update(hm[i]) > 0)
                        {
                            bol = true;
                        }
                        else
                        {
                            bol = false;
                            break;
                        }
                    }
                }
                else
                {
                    bol = false;
                }
                if (bol)
                {
                    ts.Complete();
                    return Content("true");
                }
                else {
                    return Content("false");
                }
            }
        }
        //薪酬标准查询
        public ActionResult salarystandard_query_locate() {
            return View();
        }
        //薪酬标准查询
        [HttpGet]
        public ActionResult salarystandard_query_list(string standardId, string startDate, string endDate) {
            ViewData["standardId"] = standardId;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            return View();
        }
        //薪酬标准查询
        [HttpPost]
        public ActionResult salarystandard_query_list(string standardId,string startDate,string endDate,int currentpage)
        {
            return getLike(standardId, startDate, endDate, currentpage);
        }
        //薪酬标准登记查询
        [HttpGet]
        public ActionResult salarystandard_query(string id) {
            List<salary_standard_details> list = isd.SelectWhere(e => e.standard_id == id);
            ViewData["alary_details"] = list;
            List<salary_standard> sary_stan = issbll.SelectWhere(e => e.standard_id == id);
            ViewData["standard"] = sary_stan;
            return View();
        }
        //薪酬标准变更
        public ActionResult salarystandard_change_locate() {
            return View();
        }
        //薪酬标准变更
        [HttpGet]
        public ActionResult salarystandard_change_list(string standardId, string startDate, string endDate) {
            ViewData["standardId"] = standardId;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            return View();
        }
        //薪酬标准变更
        [HttpPost]
        public ActionResult salarystandard_change_list(string standardId, string startDate, string endDate, int currentpage)
        {
            return getLike(standardId, startDate, endDate, currentpage);
        }
        //条件模糊查询
        private ActionResult getLike(string standardId, string startDate, string endDate, int currentpage)
        {
            if (startDate != "" && endDate == "")
            {
                DateTime dtime = Convert.ToDateTime(startDate);
                return Content(getList(e => e.standard_id.Contains(standardId) && e.regist_time >= dtime && e.check_status == 1, currentpage));
            }
            if (endDate != "" && startDate == "")
            {
                DateTime dtime2 = Convert.ToDateTime(endDate);
                return Content(getList(e => e.standard_id.Contains(standardId) && e.regist_time <= dtime2 && e.check_status == 1, currentpage));
            }
            if (endDate != "" && startDate != "")
            {
                DateTime dtime = Convert.ToDateTime(startDate);
                DateTime dtime2 = Convert.ToDateTime(endDate);
                return Content(getList(e => e.standard_id.Contains(standardId) && e.regist_time >= dtime && e.regist_time <= dtime2 && e.check_status == 1, currentpage));
            }
            return Content(getList(e => e.standard_id.Contains(standardId) && e.check_status == 1, currentpage));
        }

        //薪酬标准变更
        [HttpGet]
        public ActionResult salarystandard_change(string id) {
            List<salary_standard_details> list = isd.SelectWhere(e => e.standard_id == id);
            ViewData["alary_details"] = list;
            List<salary_standard> sary_stan = issbll.SelectWhere(e => e.standard_id == id);
            ViewData["standard"] = sary_stan;
            ViewData["user"] = "zhangsan";
            ViewData["list"] = isbll.SelectAll();
            ViewData["users"] = iuser.SelectAll();
            return View();
        }
        //变更中
        public ActionResult salarystandard_change_success() {
            using (TransactionScope ts = new TransactionScope())
            {
                // 获取ui传过来的数据，修改薪酬标准基本信息表成功后再一行一行删除详情，任何添加详情 ，如果出错则ts事务不提交
                string standard_id = Request.Form["standard_id"];
                string standard_name = Request.Form["standard_name"];
                string designer = Request.Form["designer"];
                string register = Request.Form["register"];
                string regist_time = Request.Form["regist_time"];
                string salary_sum = Request.Form["salary_sum"];
                string remark = Request.Form["remark"];
                //Array sdtid = Request.Form["sdtid"].Split(',');
                Array salaryid = Request.Form["salaryid"].Split(',');
                Array details = Request.Form["details"].Split(',');
                Array salarymoney = Request.Form["salarymoney"].Split(',');
                salary_standard sd = issbll.SelectWhere(e=>e.standard_id==standard_id)[0];
                sd.standard_id = standard_id;
                sd.standard_name = standard_name;
                sd.designer = designer;
                sd.changer = register;
                sd.change_time = Convert.ToDateTime(regist_time);
                sd.salary_sum = Convert.ToDecimal(salary_sum);
                sd.remark = remark;
                sd.change_status = 3;
                sd.check_status = 0;
                bool bol = true;
                if (issbll.Update(sd) > 0)
                {
                    bool del = true;
                    List<salary_standard_details> delde = isd.SelectWhere(e=>e.standard_id==standard_id);
                    for (int i = 0; i < delde.Count; i++)
                    {
                        if (isd.Delete(delde[i]) > 0)
                        {
                            del = true;
                        }
                        else {
                            del = false;
                            break;
                        }
                    }
                    if (del)
                    {
                        for (int i = 0; i < salaryid.Length; i++)
                        {
                            int sh = Convert.ToInt32(salaryid.GetValue(i));
                            //int sdtidtwo = Convert.ToInt32(sdtid.GetValue(i));
                            salary_standard_details ssd = new salary_standard_details();
                            ssd.standard_id = standard_id;
                            ssd.standard_name = standard_name;
                            ssd.item_id = (short)(sh);
                            ssd.item_name = details.GetValue(i).ToString();
                            ssd.salary = Convert.ToDecimal(salarymoney.GetValue(i));
                            if (isd.Add(ssd) > 0)
                            {
                                bol = true;
                            }
                            else
                            {
                                bol = false;
                                break;
                            }
                        }
                    }
                    else {
                        bol = false;
                    }
                }
                else
                {
                    bol = false;
                }
                if (bol)
                {
                    ts.Complete();
                    return Content("true");  return Content("false");
                }
                else
                {
                    return Content("false");
                }
            };
        }
        //变更成功
        public ActionResult salarystandard_change_success_cg() {
            return View();
        }
    }
}