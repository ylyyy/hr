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
    public class salaryCriterionController : Controller                                         
    {
        Isalary_standard_detailsBll isd = IocContain.CreateAll<Isalary_standard_detailsBll>("yibll", "ssdbll");
        // GET: salaryCriterion
        //薪酬标准登记
        public ActionResult salarystandard_register()
        {
            ViewData["user"] = "zhangsan";
            ViewData["salary_bhao"]=isd.bianHao();
            List<salary_standard_details> list= isd.SelectAll();
            ViewData["list"] = list;
            return View();
        }
        public ActionResult salarystandard_register_success(string details0itemName,string details0salary, string standard_id,string standard_name,string designer,string register,string regist_time,string salary_sum,string remark) {
            salary_standard sd = new salary_standard() {
                standard_id=standard_id,standard_name=standard_name,designer=designer,register=register,regist_time=Convert.ToDateTime(regist_time),salary_sum=Convert.ToDecimal(salary_sum),remark=remark
            };
            salary_standard_details ssd = new salary_standard_details() {
                standard_id=standard_id,standard_name=standard_name,item_id=1,item_name="",salary=Convert.ToDecimal(details0salary)
            };
            if (isd.Add(ssd) > 0)
            {
                return Content("<script>alert('成功！');</script>");
            }
            else {
                return Content("<script>alert('失败！');</script>");
            }
        }

    }
}