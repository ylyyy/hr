using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using System.Transactions;

namespace UI.Controllers
{
    public class transferController : Controller
    {
        Ihuman_fileBll human = IocContain.CreateAll<Ihuman_fileBll>("yibll", "humanbll");
        ISalary_strandardBll issbll = IocContain.CreateAll<ISalary_strandardBll>("yibll", "ssarybll");
        major_changeIBLL mcbll = IocContain.CreateAll<major_changeIBLL>("yibll", "major_changeBll");
        Iconfig_file_first_kindBLL iffk = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        Iconfig_file_second_kindBLL ifsk = IocContain.CreateAll<Iconfig_file_second_kindBLL>("yibll", "config_file_second_kindBLL");
        Iconfig_file_third_kindBLL iftk = IocContain.CreateAll<Iconfig_file_third_kindBLL>("yibll", "config_file_third_kindBLL");
        Iconfig_major_kindBLL ifmk = IocContain.CreateAll<Iconfig_major_kindBLL>("yibll", "config_major_kindBLL");
        Iconfig_majorBLL ifm = IocContain.CreateAll<Iconfig_majorBLL>("yibll", "config_majorBLL");


        // GET: transfer
        //调动登记查询
        public ActionResult register_locate()
        {
            return View();
        }
        //调动登记列表
        public ActionResult register_list(string firstKindId,string secondKindId,string thirdKindId,string startDate,string endDate)
        {
            string[] tiao = { firstKindId, secondKindId, thirdKindId, startDate, endDate };
            Session["tiaoJian"] = tiao;
            return View();
        }
        public ActionResult register_xiangQing(int currentPage)
        {
            int rows = 0;
            Dictionary<string, object> di = new Dictionary<string, object>();
            List<human_file> list = new List<human_file>();
            string[] s = Session["tiaoJian"] as string[];
            if (s[0] == "0"|| s[1]=="0"|| s[2] == "0" || s[3] == "" || s[4] == "")
            {
                list = human.FenYe(e => e.check_status == 1, out rows, currentPage, 2);
            }
            else
            {
                DateTime t1 = Convert.ToDateTime(s[3]);
                DateTime t2 = Convert.ToDateTime(s[4]);
                list = human.FenYe(e => e.first_kind_id == s[0] &&e.check_status ==1&& e.second_kind_id == s[1] && e.third_kind_id == s[2] && e.regist_time >= t1 && e.regist_time <= t2, out rows, currentPage, 2);
            }
            di.Add("rows", rows);
            di.Add("list", list);
            di.Add("pages", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            return Content(JsonConvert.SerializeObject(di));
        }
        public ActionResult register(int id)
        {
            human_file hf = human.SelectWhere(e=>e.huf_id==id).FirstOrDefault();
            ViewData["human_file"] = hf;
            return View();
        }
        public ActionResult biaozhuen()
        {
            return Content(JsonConvert.SerializeObject(issbll.SelectAll()));
        }
        public ActionResult zonge(string standard_id)
        {
            salary_standard s = issbll.SelectWhere(e=>e.standard_id == standard_id).FirstOrDefault();
            return Content(JsonConvert.SerializeObject(s));
        }
        public ActionResult registerAddUpdate(major_change mc)
        {
            if (mc.major_kind_id == mc.new_major_kind_id && mc.major_id == mc.new_major_id)
            {
                return Content("<script>alert('不可以调动到当前单位');</script>");
            }
            else
            {
                major_change mc1 = mcbll.SelectWhere(e => e.human_id == mc.human_id).FirstOrDefault();
                config_file_first_kind cffk = iffk.SelectWhere(e => e.first_kind_id == mc.new_first_kind_id).FirstOrDefault();
                config_major_kind cmk = ifmk.SelectWhere(e => e.major_kind_id == mc.new_major_kind_id).FirstOrDefault();
                salary_standard s = issbll.SelectWhere(e => e.standard_id== mc.new_salary_standard_id).FirstOrDefault();
                config_major cm = ifm.SelectWhere(e => e.major_id == mc.new_major_id && e.major_kind_id == mc.new_major_kind_id).FirstOrDefault();
                if (mc1 == null)
                {
                    if (mc.new_second_kind_id != "0")
                    {
                        config_file_second_kind cfsk = ifsk.SelectWhere(e => e.second_kind_id == mc.new_second_kind_id && e.first_kind_id == mc.new_first_kind_id).FirstOrDefault();
                        mc.new_second_kind_name = cfsk.second_kind_name;
                    }
                    if (mc.new_third_kind_id!= "0")
                    {
                        config_file_third_kind cftk = iftk.SelectWhere(e => e.third_kind_id == mc.new_third_kind_id && e.second_kind_id == mc.new_second_kind_id && e.first_kind_id == mc.first_kind_id).FirstOrDefault();
                        mc.new_third_kind_name = cftk.third_kind_name;
                    }
                    mc.new_first_kind_name = cffk.first_kind_name;
                    mc.new_major_kind_name = cmk.major_kind_name;
                    mc.new_major_name = cm.major_name;
                    mc.new_salary_standard_name = s.standard_name;
                    mc.regist_time = DateTime.Now;
                    mc.check_status = 0;
                    if (mcbll.Add(mc) > 0)
                    {
                        return Content("<script>alert('调动成功');location.href='/transfer/register_success';</script>");
                    }
                    else
                    {
                        return View("<script>alert('调动失败');</script>");
                    }
                }
                else
                {
                    if (mc.new_second_kind_id != "0")
                    {
                        config_file_second_kind cfsk = ifsk.SelectWhere(e => e.second_kind_id == mc.new_second_kind_id && e.first_kind_id == mc.new_first_kind_id).FirstOrDefault();
                        mc1.new_second_kind_id = cfsk.second_salary_id;
                        mc1.new_second_kind_name = cfsk.second_kind_name;
                    }
                    if (mc.new_third_kind_id != "0")
                    {
                        config_file_third_kind cftk = iftk.SelectWhere(e => e.third_kind_id == mc.new_third_kind_id && e.second_kind_id == mc.new_second_kind_id && e.first_kind_id == mc.first_kind_id).FirstOrDefault();
                        mc1.new_third_kind_id = cftk.third_kind_id;
                        mc1.new_third_kind_name = cftk.third_kind_name;
                    }
                    mc1.new_first_kind_id = cffk.first_kind_id;
                    mc1.new_first_kind_name = cffk.first_kind_name;
                    mc1.new_major_kind_id = cmk.major_kind_id;
                    mc1.new_major_kind_name = cmk.major_kind_name;
                    mc1.new_major_id = cmk.major_kind_id;
                    mc1.new_major_name = cm.major_name;
                    mc1.new_salary_standard_id = s.standard_id;
                    mc1.new_salary_standard_name = s.standard_name;
                    mc1.regist_time = DateTime.Now;
                    mc1.check_status = 0;
                    if (mcbll.Update(mc1) > 0)
                    {
                        return Content("<script>alert('调动成功');location.href='/transfer/register_success';</script>");
                    }
                    else
                    {
                        return View("<script>alert('调动失败');</script>");
                    }
                }
            }
        }
        
        public ActionResult register_success()
        {
            return View();
        }
        public ActionResult check_list()
        {
            return View();
        }
        public ActionResult check_listALL(int currentPage) {
            int rows = 0;
            Dictionary<string, object> di = new Dictionary<string, object>();
            List<major_change> list = mcbll.FenYe(e=>e.mch_id,e =>e.check_status == 0,out rows,currentPage,2);
            di.Add("rows", rows);
            di.Add("list", list);
            di.Add("pages", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            return Content(JsonConvert.SerializeObject(di));
        }
        public ActionResult check(int id)
        {
            major_change mc1 = mcbll.SelectWhere(e => e.mch_id == id).FirstOrDefault();
            return View(mc1);
        }
        public ActionResult checkUpdate(bool checkStatus, major_change mc)
        {
            using (TransactionScope ts = new TransactionScope()) {
                human_file hf = human.SelectWhere(e => e.human_id == mc.human_id).FirstOrDefault();
                config_file_first_kind cffk = iffk.SelectWhere(e => e.first_kind_id == mc.new_first_kind_id).FirstOrDefault();
                config_major_kind cmk = ifmk.SelectWhere(e => e.major_kind_id == mc.new_major_kind_id).FirstOrDefault();
                salary_standard s = issbll.SelectWhere(e => e.standard_id == mc.new_salary_standard_id).FirstOrDefault();
                config_major cm = ifm.SelectWhere(e => e.major_id == mc.new_major_id && e.major_kind_id == mc.new_major_kind_id).FirstOrDefault();
                config_file_second_kind cfsk = ifsk.SelectWhere(e => e.second_kind_id == mc.new_second_kind_id && e.first_kind_id == mc.new_first_kind_id).FirstOrDefault();
                config_file_third_kind cftk = iftk.SelectWhere(e => e.third_kind_id == mc.new_third_kind_id && e.second_kind_id == mc.new_second_kind_id && e.first_kind_id == mc.first_kind_id).FirstOrDefault();
                if (checkStatus)
                {
                    if (mc.new_second_kind_id != "0")
                    {
                        mc.new_second_kind_name = cfsk.second_kind_name;
                        hf.second_kind_id = cfsk.second_kind_id;
                        hf.second_kind_name= cfsk.second_kind_name;
                    }
                    if (mc.new_third_kind_id != "0")
                    {
                        hf.third_kind_id = cftk.third_kind_id;
                        hf.third_kind_name = cftk.third_kind_name;
                        mc.new_third_kind_name = cftk.third_kind_name;
                    }
                    mc.new_first_kind_name = cffk.first_kind_name;
                    mc.new_major_kind_name = cmk.major_kind_name;
                    mc.new_major_name = cm.major_name;
                    mc.new_salary_standard_name = s.standard_name;
                    mc.check_status = 1;
                    hf.first_kind_id = cffk.first_kind_id;
                    hf.first_kind_name = cffk.first_kind_name;
                    hf.human_major_kind_id = cmk.major_kind_id;
                    hf.human_major_kind_name = cmk.major_kind_name;
                    hf.human_major_id = cm.major_kind_id;
                    hf.hunma_major_name = cm.major_name;
                    hf.salary_standard_id = s.standard_id;
                    hf.salary_standard_name = s.standard_name;
                    hf.salary_sum = s.salary_sum;
                    int a = mcbll.Update(mc);
                    int b = human.Update(hf);
                    if ( a+b> 1)
                    {
                        ts.Complete();
                        return Content("<script>location.href='/transfer/check_success';</script>");
                    }
                    else
                    {
                        return View("<script>alert('审核失败');</script>");
                    }
                }
                else
                {
                    if (mc.new_second_kind_id != "0")
                    {
                        mc.new_second_kind_name = cfsk.second_kind_name;
                    }
                    if (mc.new_third_kind_id != "0")
                    {
                        mc.new_third_kind_name = cftk.third_kind_name;
                    }
                    mc.new_first_kind_name = cffk.first_kind_name;
                    mc.new_major_kind_name = cmk.major_kind_name;
                    mc.new_major_name = cm.major_name;
                    mc.new_salary_standard_name = s.standard_name;
                    mc.check_status = 2;
                    if (mcbll.Update(mc)> 0)
                    {
                        ts.Complete();
                        return Content("<script>location.href='/transfer/check_success';</script>");
                    }
                    else
                    {
                        return View("<script>alert('审核失败');</script>");
                    }
                }
            }
        }
        public ActionResult check_success() {
            return View();
        }
        public ActionResult locate() {
            ViewData["first"] = iffk.SelectAll();
            ViewData["major_kind"] = ifmk.SelectAll();
            return View();
        }
        public ActionResult locate_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate)
        {
            List<major_change> mc = null;
            if (startDate != "" && endDate == "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                mc = mcbll.SelectWhere(e => e.new_first_kind_id.Contains(firstKindId) && e.new_second_kind_id.Contains(secondKindId) && e.new_third_kind_id.Contains(thirdKindId) && e.new_major_kind_id.Contains(humanMajorKindId) && e.new_major_id.Contains(humanMajorId) && e.regist_time >= start && e.check_status == 1);
                return View(mc);
            }
            else if (startDate == "" && endDate != "")
            {
                DateTime end = Convert.ToDateTime(endDate);
                mc = mcbll.SelectWhere(e => e.new_first_kind_id.Contains(firstKindId) && e.new_second_kind_id.Contains(secondKindId) && e.new_third_kind_id.Contains(thirdKindId) && e.new_major_kind_id.Contains(humanMajorKindId) && e.new_major_id.Contains(humanMajorId) && e.regist_time <= end && e.check_status == 1);
                return View(mc);
            }
            else if (startDate != "" && endDate != "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                DateTime end = Convert.ToDateTime(endDate);
                mc = mcbll.SelectWhere(e => e.new_first_kind_id.Contains(firstKindId) && e.new_second_kind_id.Contains(secondKindId) && e.new_third_kind_id.Contains(thirdKindId) && e.new_major_kind_id.Contains(humanMajorKindId) && e.new_major_id.Contains(humanMajorId) && e.regist_time >= start && e.regist_time <= end && e.check_status == 1);
                return View(mc);

            }
            else
            {
                mc = mcbll.SelectWhere(e => e.new_first_kind_id.Contains(firstKindId) && e.new_second_kind_id.Contains(secondKindId) && e.new_third_kind_id.Contains(thirdKindId) && e.new_major_kind_id.Contains(humanMajorKindId) && e.new_major_id.Contains(humanMajorId) && e.check_status == 1);
                return View(mc);
            }
        }

        public  ActionResult detail(int id)
        {
            major_change  mc= mcbll.SelectWhere(e=>e.mch_id==id).FirstOrDefault();
            return View(mc);
        }
    }
}