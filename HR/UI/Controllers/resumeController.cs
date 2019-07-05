using Entity;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class resumeController : Controller
    {
        Iconfig_major_kindBLL ifmk = IocContain.CreateAll<Iconfig_major_kindBLL>("yibll", "config_major_kindBLL");
        Iconfig_majorBLL ifm = IocContain.CreateAll<Iconfig_majorBLL>("yibll", "config_majorBLL");
        Iengage_major_releaseBLL imr = IocContain.CreateAll<Iengage_major_releaseBLL>("yibll", "engage_major_releaseBLL");
        Iengage_resumeBLL irb = IocContain.CreateAll<Iengage_resumeBLL>("yibll", "engage_resumeBLL");
        Iconfig_public_charBLL ipcc = IocContain.CreateAll<Iconfig_public_charBLL>("yibll", "config_public_charBLL");
        IusersBll iub = IocContain.CreateAll<IusersBll>("yibll", "userbll");
        // GET: resume
        //简历登记
        #region
        public ActionResult register()
        {
            NewMethod();
            return View();
        }
        public ActionResult registerIndex(engage_major_release mre)
        {
            engage_major_release er = imr.SelectWhere(e => e.mre_id == mre.mre_id).FirstOrDefault();
            ViewData["emr"] = er;
            return View();
        }
        private void NewMethod()
        {
            List<SelectListItem> lists1 = new List<SelectListItem>();
            List<SelectListItem> lists2 = new List<SelectListItem>();

            List<config_major_kind> list = ifmk.SelectAll();
            List<config_major> list1 = ifm.SelectAll();
            foreach (config_major_kind cmk in list)
            {
                SelectListItem s = new SelectListItem
                {
                    Text = cmk.major_kind_name,
                    Value = cmk.major_kind_id,
                };
                lists1.Add(s);
            }
            foreach (config_major cmk in list1)
            {
                SelectListItem s = new SelectListItem
                {
                    Text = cmk.major_name,
                    Value = cmk.major_id,
                };
                lists2.Add(s);
            }
            ViewData["feiLei1"] = lists1;
            ViewData["feiLei2"] = lists2;
        }

        public ActionResult position_registerZW()
        {
            List<config_major_kind> list = ifmk.SelectAll();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult position_registerZWFL(string major_kind_id)
        {
            List<config_major> list = ifm.SelectWhere(e => e.major_kind_id == major_kind_id);
            return Content(JsonConvert.SerializeObject(list));
        }

        public ActionResult registerIndex1()
        {
            List<config_public_char> list = ipcc.SelectWhere(e => e.attribute_kind == "国籍");
            List<config_public_char> list1 = ipcc.SelectWhere(e => e.attribute_kind == "宗教信仰");
            List<config_public_char> list2 = ipcc.SelectWhere(e => e.attribute_kind == "政治面貌");
            List<config_public_char> list3 = ipcc.SelectWhere(e => e.attribute_kind == "教育年限");
            List<config_public_char> list4 = ipcc.SelectWhere(e => e.attribute_kind == "专业");
            List<config_public_char> list5 = ipcc.SelectWhere(e => e.attribute_kind == "特长");
            List<config_public_char> list6 = ipcc.SelectWhere(e => e.attribute_kind == "爱好");
            List<config_public_char> list7 = ipcc.SelectWhere(e => e.attribute_kind == "学历");
            List<config_public_char> list8 = ipcc.SelectWhere(e => e.attribute_kind == "民族");
            Dictionary<string, object> di = new Dictionary<string, object>();
            di.Add("list", list);
            di.Add("list1", list1);
            di.Add("list2", list2);
            di.Add("list3", list3);
            di.Add("list4", list4);
            di.Add("list5", list5);
            di.Add("list6", list6);
            di.Add("list7", list7);
            di.Add("list8", list8);
            return Content(JsonConvert.SerializeObject(di));
        }
        public ActionResult registerAdd(engage_resume er)
        {
            //int b=TimeSpan.Compare((TimeSpan)er.regist_time, (TimeSpan)er.human_birthday);
            users u = Session["user"] as users;
            er.check_status = 0;
            er.interview_status = 0;
            er.register = u.u_true_name;
            er.regist_time = DateTime.Now;
            if (er.human_birthday.ToString() != "")
            {
                DateTime t1 = (DateTime)er.regist_time;
                DateTime t2 = (DateTime)er.human_birthday;
                er.human_age = (short)(t1.Year - t2.Year);
            }
            if (irb.Add(er) > 0)
            {
                return Content("<script>alert('添加成功');location.href='/resume/register';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');location.href='/resume/register';</script>");
            }
        }
        public ActionResult registerAdd1(engage_resume er)
        {
            config_major cm = ifm.SelectWhere(e => e.major_id == er.human_major_id && e.major_kind_id == er.human_major_kind_id).FirstOrDefault();
            er.human_major_kind_name = cm.major_kind_name;
            er.human_major_name = cm.major_name;
            users u = Session["user"] as users;
            er.check_status = 0;
            er.interview_status = 0;
            er.register = u.u_true_name;
            er.regist_time = DateTime.Now;
            if (er.human_birthday.ToString() != "")
            {
                DateTime t1 = (DateTime)er.regist_time;
                DateTime t2 = (DateTime)er.human_birthday;
                er.human_age = (short)(t1.Year - t2.Year);
            }
            if (irb.Add(er) > 0)
            {
                return Content("<script>alert('添加成功');location.href='/resume/register';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');location.href='/resume/register';</script>");
            }
        }
        #endregion
        //简历筛选
        #region
        public ActionResult resume_choose()
        {
            return View();
        }
        public ActionResult resume_list()
        {
            //string humanMajorKindId,string humanMajorId,string primarKey,DateTime startDate, DateTime uendDate
            string humanMajorKindId = Request["humanMajorKindId"];
            string humanMajorId = Request["humanMajorId"];
            string primarKey = Request["primarKey"];
            string startDate = Request["startDate"];
            string uendDate = Request["uendDate"];
            List<engage_resume> ers = null;
            if (humanMajorKindId == null && humanMajorId == null && startDate == "" && uendDate == "")
            {
                ers = irb.SelectWhere(e => e.check_status == 0 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (humanMajorKindId == null)
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.check_status == 0 && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
                //ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 0 && e.regist_time >=startDate && e.regist_time <= uendDate && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (humanMajorId == null)
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 0 && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (startDate == "")
            {
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 0 && e.human_major_id == humanMajorId && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (uendDate == "")
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 0 && e.human_major_id == humanMajorId && e.regist_time >= t1 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(uendDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 0 && e.human_major_id == humanMajorId && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            return View(ers);
        }

        public ActionResult Daifuhe(engage_resume err)
        {
            err.check_status = 1;
            err.interview_status = 0;
            DateTime t1 = (DateTime)err.regist_time;
            DateTime t2 = (DateTime)err.human_birthday;
            err.human_age = (short)(t1.Year - t2.Year);
            if (irb.Update(err) > 0)
            {
                return Content("<script>alert('复核成功');location.href='/resume/resume_choose';</script>");
            }
            else
            {
                return Content("<script>alert('复核失败');location.href='/resume/resume_choose';</script>");
            }
        }
        public ActionResult resume_details(int id)
        {
            engage_resume rb = irb.SelectWhere(e => e.res_id == id).FirstOrDefault();
            List<SelectListItem> list = new List<SelectListItem>();
            List<users> list1 = iub.SelectAll();
            foreach (users u in list1)
            {
                SelectListItem s = new SelectListItem
                {
                    Text = u.u_true_name,
                    Value = u.u_true_name,
                };
                list.Add(s);
            }
            ViewData["s"] = list;
            return View(rb);
        }
        #endregion
        //有效简历查询
        #region
        public ActionResult valid_resume()
        {
            return View();
        }
        public ActionResult valid_list()
        {
            string humanMajorKindId = Request["humanMajorKindId"];
            string humanMajorId = Request["humanMajorId"];
            string primarKey = Request["primarKey"];
            string startDate = Request["startDate"];
            string endDate = Request["endDate"];
            List<engage_resume> ers = null;
            if (humanMajorKindId == null && humanMajorId == null && startDate == "" && endDate == "")
            {
                ers = irb.SelectWhere(e => e.check_status == 1 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (humanMajorKindId == null)
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(endDate);
                ers = irb.SelectWhere(e => e.check_status == 1 && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
                //ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 0 && e.regist_time >=startDate && e.regist_time <= uendDate && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (humanMajorId == null)
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(endDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (startDate == "")
            {
                DateTime t2 = Convert.ToDateTime(endDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.human_major_id == humanMajorId && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else if (endDate == "")
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.human_major_id == humanMajorId && e.regist_time >= t1 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            else
            {
                DateTime t1 = Convert.ToDateTime(startDate);
                DateTime t2 = Convert.ToDateTime(endDate);
                ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status == 0 && e.human_major_id == humanMajorId && e.regist_time >= t1 && e.regist_time <= t2 && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            }
            return View(ers);
            //List<engage_resume> ers = irb.SelectWhere(e => e.human_major_kind_id == humanMajorKindId && e.check_status == 1 && e.interview_status==0 && e.human_major_id == humanMajorId && e.regist_time >= startDate && e.regist_time <= endDate && (e.human_name.Contains(primarKey) || e.human_telephone.Contains(primarKey) || e.human_idcard.Contains(primarKey) || e.human_history_records.Contains(primarKey)));
            //return View(ers);
        }
        public ActionResult resume_select(int id)
        {
            engage_resume rb = irb.SelectWhere(e => e.res_id == id).FirstOrDefault();
            return View(rb);
        }
        #endregion
    }
}