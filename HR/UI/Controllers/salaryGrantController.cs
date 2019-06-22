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
using System.Transactions;
using System.Linq.Expressions;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class salaryGrantController : Controller
    {

        Iconfig_file_first_kindBLL ifirst = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        Iconfig_file_second_kindBLL itwo = IocContain.CreateAll<Iconfig_file_second_kindBLL>("yibll", "config_file_second_kindBLL");
        Iconfig_file_third_kindBLL ithree = IocContain.CreateAll<Iconfig_file_third_kindBLL>("yibll", "config_file_third_kindBLL");

        Ihuman_fileBll human = IocContain.CreateAll<Ihuman_fileBll>("yibll", "humanbll");
        Isalary_standard_detailsBll isd = IocContain.CreateAll<Isalary_standard_detailsBll>("yibll", "ssdbll");
        ISalaryBll isbll = IocContain.CreateAll<ISalaryBll>("yibll", "sarybll");

        Isalary_grantBll grant = IocContain.CreateAll<Isalary_grantBll>("yibll", "sgrantbll");
        Isalary_grant_detailsBll grantdetail = IocContain.CreateAll<Isalary_grant_detailsBll>("yibll", "sgrantdetailbll");

        //薪酬发放登记   几级选择
        // GET: salaryGrant
        
        public ActionResult register_locate()
        {
            return View();
        }
        //薪酬发放登记    几级结构查询
        public ActionResult register_list(string submitType) {
            //判断是几级机构
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
            ViewData["hsbianhao"] = human.bianHao();
            ViewData["user"] = "zhangsan";
            ViewData["sary_details"] = isd.SelectAll();
            
            if (jg=="1")
            {
                ViewData["jg"] = jg;
                ViewData["human"]=human.SelectWhere(e=>e.first_kind_id==id);
                ViewData["first_id"]= ifirst.SelectWhere(e=>e.first_kind_id==id).FirstOrDefault().first_kind_id;
                ViewData["first_name"] = ifirst.SelectWhere(e => e.first_kind_id == id).FirstOrDefault().first_kind_name;

                ViewData["two_id"] = "";
                ViewData["two_name"] = "";

                ViewData["three_id"] = "";
                ViewData["three_name"] = "";
                return View();
            }
            else if (jg=="2")
            {
                ViewData["jg"] = jg;
                ViewData["human"] = human.SelectWhere(e => e.second_kind_id == id);
                ViewData["first_id"] = itwo.SelectWhere(e => e.second_kind_id == id).FirstOrDefault().first_kind_id;
                ViewData["first_name"] = itwo.SelectWhere(e => e.second_kind_id == id).FirstOrDefault().first_kind_name;

                ViewData["two_id"] = itwo.SelectWhere(e => e.second_kind_id == id).FirstOrDefault().second_kind_id;
                ViewData["two_name"] = itwo.SelectWhere(e => e.second_kind_id == id).FirstOrDefault().second_kind_name;

                ViewData["three_id"] = "";
                ViewData["three_name"] = "";
                return View();
            }
            else if (jg=="3")
            {
                ViewData["jg"] = jg;
                ViewData["human"] = human.SelectWhere(e=>e.third_kind_id==id);
                ViewData["first_id"] = ithree.SelectWhere(e => e.third_kind_id == id).FirstOrDefault().first_kind_id;
                ViewData["first_name"] = ithree.SelectWhere(e => e.third_kind_id == id).FirstOrDefault().first_kind_name;

                ViewData["two_id"] = ithree.SelectWhere(e => e.third_kind_id == id).FirstOrDefault().second_kind_id;
                ViewData["two_name"] = ithree.SelectWhere(e => e.third_kind_id == id).FirstOrDefault().second_kind_name;

                ViewData["three_id"] = ithree.SelectWhere(e => e.third_kind_id == id).FirstOrDefault().third_kind_id;
                ViewData["three_name"] = ithree.SelectWhere(e => e.third_kind_id == id).FirstOrDefault().third_kind_name;
                return View();
            }
            return Content("<script>alert('无页面！');</script>");
        }
        //薪酬发放登记  登记页面 工资详情
        public ActionResult register_commit_details(string id) {
            ViewData["salary"] = isd.SelectWhere(e=>e.standard_id==id);
            return View();
        }

        //薪酬发放登记 登记完成
        [HttpGet]
        public ActionResult register_success() {
            return View();
        }
        //薪酬发放登记 添加中
        [HttpPost]
        public ActionResult register_success(string salary_grant_id,string first_kind_id,string first_kind_name,string second_kind_id,string second_kind_name,string third_kind_id,string third_kind_name,string human_amount,string salary_standard_sum,string salary_paid_sum,string register,string regist_time,string human_id,string human_name,string bounsSum,string saleSum,string deductSum,string salaryPaidSum,string salary_sum)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int hm_amount = Convert.ToInt32(human_amount);
                DateTime regtime = Convert.ToDateTime(regist_time);
                decimal sss = Convert.ToDecimal(salary_standard_sum);
                decimal sps = Convert.ToDecimal(salary_paid_sum);
                salary_grant sg = new salary_grant()
                {
                    salary_grant_id = salary_grant_id,
                    first_kind_id = first_kind_id,
                    first_kind_name = first_kind_name,
                    second_kind_id = second_kind_id,
                    second_kind_name = second_kind_name,
                    third_kind_id = third_kind_id,
                    third_kind_name = third_kind_name,
                    human_amount = (short)hm_amount,
                    salary_standard_sum = sss,
                    salary_paid_sum = sps,
                    register = register,
                    regist_time = regtime,
                    check_status=0
                };
                bool bol = true;
                if (grant.Add(sg) > 0)
                {
                    Array hm_id = human_id.Split(',');
                    Array hm_name = human_name.Split(',');
                    Array bouns_sum = bounsSum.Split(',');
                    Array sale_sum = saleSum.Split(',');
                    Array deduct_sum = deductSum.Split(',');
                    Array salarypaidsum = salaryPaidSum.Split(',');
                    Array salarystandardsum = salary_sum.Split(',');
                    for (int i = 0; i < hm_id.Length; i++)
                    {
                        decimal bs = Convert.ToDecimal(bouns_sum.GetValue(i).ToString()==""?0: bouns_sum.GetValue(i));
                        decimal ss = Convert.ToDecimal(sale_sum.GetValue(i).ToString()==""?0: sale_sum.GetValue(i));
                        decimal ds = Convert.ToDecimal(deduct_sum.GetValue(i).ToString()==""?0: deduct_sum.GetValue(i));
                        decimal sssum = Convert.ToDecimal(salarystandardsum.GetValue(i).ToString()==""?0: salarystandardsum.GetValue(i));
                        decimal spsum = Convert.ToDecimal(salarypaidsum.GetValue(i).ToString()==""?0: salarypaidsum.GetValue(i));
                        salary_grant_details sgd = new salary_grant_details()
                        {
                            salary_grant_id = salary_grant_id,
                            human_id = hm_id.GetValue(i).ToString(),
                            human_name = hm_name.GetValue(i).ToString(),
                            bouns_sum = bs,
                            sale_sum = ss,
                            deduct_sum = ds,
                            salary_standard_sum = sssum,
                            salary_paid_sum = spsum
                        };
                        if (grantdetail.Add(sgd) > 0)
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
            }
        }
        //薪酬发放登记 复核显示
        [HttpGet]
        public ActionResult check_list()
        {
            return View();
        }
        [HttpPost]
        public ActionResult check_list(int currentPage) {
            return Content(getList(e => e.check_status == 0, currentPage));
        }
        //复核分页
        private string getList(Expression<Func<salary_grant, bool>> where, int currentPage)
        {
            int rows = 0;
            List<salary_grant> list = grant.FenYe(where, out rows, currentPage, 10);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            int pages = rows % 10 == 0 ? rows / 10 : rows / 10 + 1;
            dic.Add("rows", rows);
            dic.Add("pages", pages);
            dic.Add("currentpage", currentPage);
            dic.Add("list", list);
            return JsonConvert.SerializeObject(dic);
        }
        //薪酬发放登记 薪酬发放复核
        public ActionResult check(string salary_grant_id) {
            salary_grant sagrant = grant.SelectWhere(e=>e.salary_grant_id==salary_grant_id)[0];
            List<salary_grant_details> grantde=grantdetail.SelectWhere(e=>e.salary_grant_id==salary_grant_id);
            List<string> standardid = new List<string>();
            for (int i = 0; i < grantde.Count; i++)
            {
                string ghmid = grantde[i].human_id;
                human_file hmf=human.SelectWhere(e=>e.human_id ==ghmid)[0];
                standardid.Add(hmf.salary_standard_id);
            }
            ViewData["grantde"] = grantde;
            ViewData["standardid"] = standardid;
            ViewData["sagrant"] = sagrant;
            ViewData["user"] = "zhangsan";
            if (sagrant.third_kind_name != "")
            {
                ViewData["jg"] = 3;
            }
            else if (sagrant.second_kind_name != "")
            {
                ViewData["jg"] = 2;
            }
            else {
                ViewData["jg"] = 1;
            }
            return View();
        }
        //薪酬发放登记 薪酬复核通过
        [HttpGet]
        public ActionResult check_success() {
            return View();
        }
        [HttpPost]
        public ActionResult check_success(string salary_grant_id,string salary_paid_sum, string checker,string check_time, string human_id, string human_name, string bounsSum, string saleSum, string deductSum, string salaryPaidSum, string salary_sum)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                salary_grant sg = grant.SelectWhere(e=>e.salary_grant_id==salary_grant_id)[0];
                sg.salary_paid_sum =Convert.ToDecimal(salary_paid_sum);
                sg.checker = checker;
                sg.check_time =Convert.ToDateTime(check_time);
                sg.check_status = 1;
                bool bol = true;
                if (grant.Update(sg) > 0)
                {
                    bool delid = true;
                    List<salary_grant_details> sgdlist = grantdetail.SelectWhere(e=>e.salary_grant_id==salary_grant_id);
                    for (int i = 0; i < sgdlist.Count; i++)
                    {
                        if (grantdetail.Delete(sgdlist[i]) > 0)
                        {
                            delid = true;
                        }
                        else {
                            delid = false;
                            break;
                        }
                    }
                    if (delid)
                    {
                        Array hm_id = human_id.Split(',');
                        Array hm_name = human_name.Split(',');
                        Array bouns_sum = bounsSum.Split(',');
                        Array sale_sum = saleSum.Split(',');
                        Array deduct_sum = deductSum.Split(',');
                        Array salarypaidsum = salaryPaidSum.Split(',');
                        Array salarystandardsum = salary_sum.Split(',');
                        for (int i = 0; i < hm_id.Length; i++)
                        {
                            decimal bs = Convert.ToDecimal(bouns_sum.GetValue(i).ToString() == "" ? 0 : bouns_sum.GetValue(i));
                            decimal ss = Convert.ToDecimal(sale_sum.GetValue(i).ToString() == "" ? 0 : sale_sum.GetValue(i));
                            decimal ds = Convert.ToDecimal(deduct_sum.GetValue(i).ToString() == "" ? 0 : deduct_sum.GetValue(i));
                            decimal sssum = Convert.ToDecimal(salarystandardsum.GetValue(i).ToString() == "" ? 0 : salarystandardsum.GetValue(i));
                            decimal spsum = Convert.ToDecimal(salarypaidsum.GetValue(i).ToString() == "" ? 0 : salarypaidsum.GetValue(i));
                            salary_grant_details sgd = new salary_grant_details()
                            {
                                salary_grant_id = salary_grant_id,
                                human_id = hm_id.GetValue(i).ToString(),
                                human_name = hm_name.GetValue(i).ToString(),
                                bouns_sum = bs,
                                sale_sum = ss,
                                deduct_sum = ds,
                                salary_standard_sum = sssum,
                                salary_paid_sum = spsum
                            };
                            if (grantdetail.Add(sgd) > 0)
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
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
        }
        //薪酬发放查询 条件查询
        public ActionResult query_locate() {
            return View();
        }
        //薪酬发放查询 查询条件
        [HttpGet]
        public ActionResult query_list(string salaryGrantId, string startDate, string endDate) {
            ViewData["salaryGrantId"] = salaryGrantId;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            return View();
        }
        //薪酬发放查询 查询显示
        [HttpPost]
        public ActionResult query_list(string salaryGrantId,string startDate,string endDate,int currentpage) {
            if (startDate != "" && endDate == "")
            {
                DateTime dtime = Convert.ToDateTime(startDate);
                return Content(getList(e => e.salary_grant_id.Contains(salaryGrantId) && e.regist_time >= dtime && e.check_status == 1, currentpage));
            }
            if (endDate != "" && startDate == "")
            {
                DateTime dtime2 = Convert.ToDateTime(endDate);
                return Content(getList(e => e.salary_grant_id.Contains(salaryGrantId) && e.regist_time <= dtime2 && e.check_status == 1, currentpage));
            }
            if (endDate != "" && startDate != "")
            {
                DateTime dtime = Convert.ToDateTime(startDate);
                DateTime dtime2 = Convert.ToDateTime(endDate);
                return Content(getList(e => e.salary_grant_id.Contains(salaryGrantId) && e.regist_time >= dtime && e.regist_time <= dtime2 && e.check_status == 1, currentpage));
            }
            return Content(getList(e => e.salary_grant_id.Contains(salaryGrantId) && e.check_status == 1, currentpage));
        }
        //薪酬发放查询 查询grant_details
        public ActionResult query(string salary_grant_id) {
            salary_grant sagrant = grant.SelectWhere(e => e.salary_grant_id == salary_grant_id)[0];
            List<salary_grant_details> grantde = grantdetail.SelectWhere(e => e.salary_grant_id == salary_grant_id);
            List<string> standardid = new List<string>();
            for (int i = 0; i < grantde.Count; i++)
            {
                string ghmid = grantde[i].human_id;
                human_file hmf = human.SelectWhere(e => e.human_id == ghmid)[0];
                standardid.Add(hmf.salary_standard_id);
            }
            ViewData["grantde"] = grantde;
            ViewData["standardid"] = standardid;
            ViewData["sagrant"] = sagrant;
            ViewData["user"] = "zhangsan";
            if (sagrant.third_kind_name != "")
            {
                ViewData["jg"] = 3;
            }
            else if (sagrant.second_kind_name != "")
            {
                ViewData["jg"] = 2;
            }
            else
            {
                ViewData["jg"] = 1;
            }
            return View();
        }
    }
}