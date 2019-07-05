using Entity;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class interviewController : Controller
    {
        Iconfig_major_kindBLL ifmk = IocContain.CreateAll<Iconfig_major_kindBLL>("yibll", "config_major_kindBLL");
        Iconfig_majorBLL ifm = IocContain.CreateAll<Iconfig_majorBLL>("yibll", "config_majorBLL");
        Iengage_major_releaseBLL imr = IocContain.CreateAll<Iengage_major_releaseBLL>("yibll", "engage_major_releaseBLL");
        Iengage_resumeBLL irb = IocContain.CreateAll<Iengage_resumeBLL>("yibll", "engage_resumeBLL");
        Iconfig_public_charBLL ipcc = IocContain.CreateAll<Iconfig_public_charBLL>("yibll", "config_public_charBLL");
        Iengage_interviewBLL ib = IocContain.CreateAll<Iengage_interviewBLL>("yibll", "engage_interviewBLL");
        IusersBll iub = IocContain.CreateAll<IusersBll>("yibll", "userbll");
        // GET: interview
        //面试结果登记
        #region
        public ActionResult interview_list()
        {
            string humanMajorKindId = Request["humanMajorKindId"];
            string humanMajorId = Request["humanMajorId"];
            string primarKey = Request["primarKey"];
            string startDate = Request["startDate"];
            string uendDate = Request["endDate"];
            List<engage_resume> ers = null;
            if (humanMajorKindId == null && humanMajorId == null && startDate == "" && uendDate == "")
            {
                ers = irb.SelectWhere(e => e.interview_status == 0 && e.check_status == 1 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (humanMajorKindId == null)
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.interview_status == 0 && e.check_status == 1 && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
                //ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 0 && e.regist_time >=startDate && e.regist_time <= uendDate && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (humanMajorId == null)
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (startDate == "")
            {
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.human_major_id == humanMajorId && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (uendDate == "")
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.human_major_id == humanMajorId && e.regist_time >= t1 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.human_major_id == humanMajorId && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            return View(ers);
        }
        public ActionResult interview_register(int id)
        {
            engage_resume er = irb.SelectWhere(e => e.res_id == id).FirstOrDefault();
            ViewData["engage_resume"] = er;
            ViewData["c"] = ib.SelectWhere(e => e.resume_id == id).FirstOrDefault();
            return View();
        }
        public ActionResult interview_resume()
        {
            return View();
        }
        public ActionResult recruitAction(engage_interview ei)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                engage_interview eiv = ib.SelectWhere(e => e.human_name == ei.human_name).FirstOrDefault();
                engage_resume er = irb.SelectWhere(e => e.human_name == ei.human_name).FirstOrDefault();
                er.interview_status = 1;
                int a = 0;
                int b = 0;
                if (eiv == null)
                {
                    ei.resume_id = er.res_id;
                    ei.interview_status = 1;
                    a = ib.Add(ei);
                    b = irb.Update(er);
                }
                else
                {
                    eiv.interview_amount = ei.interview_amount;
                    eiv.image_degree = ei.image_degree;
                    eiv.native_language_degree = ei.native_language_degree;
                    eiv.foreign_language_degree = ei.foreign_language_degree;
                    eiv.response_speed_degree = ei.response_speed_degree;
                    eiv.EQ_degree = ei.EQ_degree;
                    eiv.IQ_degree = ei.IQ_degree;
                    eiv.multi_quality_degree = ei.multi_quality_degree;
                    eiv.registe_time = ei.registe_time;
                    eiv.interview_comment = ei.interview_comment;
                    eiv.interview_status = 1;
                    a = ib.Update(eiv);
                    b = irb.Update(er);
                }
                if (a + b > 1)
                {
                    ts.Complete();
                    return Content("<script>alert('登记成功');location.href='/interview/interview_resume';</script>");
                }
                else
                {
                    return Content("<script>alert('登记失败');</script>");
                }
            }
        }
        #endregion
        //
        public ActionResult sift_list()
        {
            return View();
        }
        public ActionResult sift_listIndex(int currentPage)
        {
            int rows = 0;
            var list = ib.FenYe(e => e.ein_id, e => e.interview_status == 1, out rows, currentPage, 2);
            Dictionary<string, object> di = new Dictionary<string, object>();
            di.Add("rows", rows);
            di.Add("list", list);
            di.Add("pages", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            return Content(JsonConvert.SerializeObject(di));
        }
        public ActionResult interview_sift(int id)
        {
            engage_interview ei = ib.SelectWhere(e => e.ein_id == id).FirstOrDefault();
            ViewData["ei"] = ei;
            engage_resume er = irb.SelectWhere(e => e.res_id == ei.resume_id).FirstOrDefault();
            List<SelectListItem> list1 = new List<SelectListItem>();
            List<users> list = iub.SelectAll();
            foreach (users u in list)
            {
                SelectListItem s = new SelectListItem()
                {
                    Text = u.u_true_name,
                    Value = u.u_true_name,
                };
                list1.Add(s);
            }
            ViewData["list1"] = list1;
            return View(er);
        }

        public ActionResult humanfile(engage_resume er)
        {

            using (TransactionScope ts = new TransactionScope())
            {
                engage_resume ere = irb.SelectWhere(e => e.res_id == er.res_id).FirstOrDefault();
                engage_interview ei = ib.SelectWhere(e => e.resume_id == er.res_id).FirstOrDefault();
                int a = 0;
                int b = 0;
                ere.pass_checkComment = er.pass_checkComment;
                ei.result = er.pass_checkComment;
                ei.checker = er.checker;
                ei.check_time = er.check_time;
                if (er.pass_checkComment == "建议面试")
                {
                    ere.interview_status = 0;
                    ei.interview_status = 0;
                    a = irb.Update(ere);
                    b = ib.Update(ei);
                }
                else if (er.pass_checkComment == "建议录用")
                {
                    ere.interview_status = 2;
                    ei.interview_status = 2;
                    a = irb.Update(ere);
                    b = ib.Update(ei);

                }
                else if (er.pass_checkComment == "删除简历")
                {
                    a = irb.Delete(ere);
                    b = ib.Delete(ei);
                }

                if (a + b > 1)
                {
                    ts.Complete();
                    return Content("<script>alert('提交成功');location.href='/interview/sift_list';</script>");
                }
                else
                {
                    return Content("<script>alert('提交失败');</script>");
                }
            }
        }
    }
}