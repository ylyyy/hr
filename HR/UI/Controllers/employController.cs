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
    public class employController : Controller
    {
        Iengage_resumeBLL irb = IocContain.CreateAll<Iengage_resumeBLL>("yibll", "engage_resumeBLL");
        Iengage_interviewBLL ib = IocContain.CreateAll<Iengage_interviewBLL>("yibll", "engage_interviewBLL");
        // GET: employ
        public ActionResult register_list()
        {
            return View();
        }
        public ActionResult register_listIndex(int currentPage, int interview_status)
        {
            int rows = 0;
            var list = irb.FenYe(e => e.res_id, e => e.interview_status == interview_status, out rows, currentPage, 2);
            Dictionary<string, object> di = new Dictionary<string, object>();
            di.Add("rows", rows);
            di.Add("list", list);
            di.Add("pages", rows % 2 > 0 ? (rows / 2) + 1 : (rows / 2));
            return Content(JsonConvert.SerializeObject(di));
        }
        public ActionResult register(int id)
        {
            engage_resume er = irb.SelectWhere(e => e.res_id == id).FirstOrDefault();
            engage_interview ei = ib.SelectWhere(e => e.resume_id == id).FirstOrDefault();
            ViewData["ei"] = ei;
            return View(er);
        }
        public ActionResult recruitAction(engage_resume ers)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                engage_resume ere = irb.SelectWhere(e => e.res_id == ers.res_id).FirstOrDefault();
                engage_interview ei = ib.SelectWhere(e => e.resume_id == ers.res_id).FirstOrDefault();
                ere.pass_checkComment = ers.pass_checkComment;
                if (ers.pass_checkComment == "通过")
                {
                    ere.interview_status = 3;
                    ei.interview_status = 3;
                }
                else
                {
                    ere.interview_status = 1;
                    ei.interview_status = 1;
                }
                if (irb.Update(ere) + ib.Update(ei) > 1)
                {
                    ts.Complete();
                    return Content("<script>alert('提交成功');location.href='/employ/register_list';</script>");
                }
                else
                {
                    return Content("<script>alert('提交失败');</script>");
                }
            }
        }

        public ActionResult check_list()
        {
            return View();
        }
        public ActionResult check(int id)
        {
            engage_resume er = irb.SelectWhere(e => e.res_id == id).FirstOrDefault();
            engage_interview ei = ib.SelectWhere(e => e.resume_id == id).FirstOrDefault();
            ViewData["ei"] = ei;
            return View(er);
        }
        public ActionResult returnCheckList(engage_resume ers)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                engage_resume ere = irb.SelectWhere(e => e.res_id == ers.res_id).FirstOrDefault();
                engage_interview ei = ib.SelectWhere(e => e.resume_id == ers.res_id).FirstOrDefault();
                ere.pass_passComment = ers.pass_passComment;
                if (ers.pass_passComment == "通过")
                {
                    ere.interview_status = 4;
                    ei.interview_status = 4;
                }
                else
                {
                    ere.interview_status = 5;
                    ei.interview_status = 5;
                }
                if (irb.Update(ere) + ib.Update(ei) > 1)
                {
                    ts.Complete();
                    return Content("<script>alert('提交成功');location.href='/employ/check_list';</script>");
                }
                else
                {
                    return Content("<script>alert('提交失败');</script>");
                }
            }
        }

        public ActionResult list()
        {
            return View();
        }
        public ActionResult details(int id)
        {
            engage_resume er = irb.SelectWhere(e => e.res_id == id).FirstOrDefault();
            engage_interview ei = ib.SelectWhere(e => e.resume_id == id).FirstOrDefault();
            ViewData["ei"] = ei;
            return View(er);
        }
    }
}