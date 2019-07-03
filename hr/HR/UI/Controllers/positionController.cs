using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IocContainer;
using IBLL;
using Entity;
using Newtonsoft.Json;

namespace UI.Controllers
{
    public class positionController : Controller
    {
        // GET: position
        Iconfig_file_first_kindBLL iffk = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        Iconfig_file_second_kindBLL ifsk = IocContain.CreateAll<Iconfig_file_second_kindBLL>("yibll", "config_file_second_kindBLL");
        Iconfig_file_third_kindBLL iftk = IocContain.CreateAll<Iconfig_file_third_kindBLL>("yibll", "config_file_third_kindBLL");
        Iconfig_major_kindBLL ifmk = IocContain.CreateAll<Iconfig_major_kindBLL>("yibll", "config_major_kindBLL");
        Iconfig_majorBLL ifm = IocContain.CreateAll<Iconfig_majorBLL>("yibll", "config_majorBLL");
        Iengage_major_releaseBLL imr = IocContain.CreateAll<Iengage_major_releaseBLL>("yibll", "engage_major_releaseBLL");
        IusersBLL iub = IocContain.CreateAll<IusersBLL>("yibll", "usersBLL");
        //职位发布登记
        #region
        public ActionResult position_register()
        {
            return View();
        }
        public ActionResult position_registerYi()
        {
            List<config_file_first_kind> list = iffk.SelectAll();
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult position_registerEr(string first_kind_id)
        {
            List<config_file_second_kind> list = ifsk.SelectWhere(e=>e.first_kind_id==first_kind_id);
            return Content(JsonConvert.SerializeObject(list));
        }
        public ActionResult position_registerSan(string second_kind_id)
        {
            List<config_file_third_kind> list = iftk.SelectWhere(e => e.second_kind_id == second_kind_id);
            return Content(JsonConvert.SerializeObject(list));
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
        public ActionResult position_registerTJ(engage_major_release emr)
        {
            config_file_first_kind cffk = iffk.SelectWhere(e=>e.first_kind_id==emr.first_kind_id).FirstOrDefault();
            config_file_second_kind cfsk = ifsk.SelectWhere(e => e.second_kind_id == emr.second_kind_id).FirstOrDefault();
            config_file_third_kind cftk = iftk.SelectWhere(e => e.third_kind_id == emr.third_kind_id).FirstOrDefault();
            config_major_kind cmk = ifmk.SelectWhere(e => e.major_kind_id == emr.major_kind_id).FirstOrDefault();
            config_major cm = ifm.SelectWhere(e => e.major_id == emr.major_id&&e.major_kind_id==emr.major_kind_id).FirstOrDefault();
            emr.first_kind_name = cffk.first_kind_name;
            emr.second_kind_name = cfsk.second_kind_name;
            emr.third_kind_name = cftk.third_kind_name;
            emr.major_kind_name = cmk.major_kind_name;
            emr.major_name = cm.major_name;
            emr.regist_time = DateTime.Now;
            if (imr.Add(emr) > 0)
            {
                return Content("<script>alert('添加成功');location.href='/position/position_change_update';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');</script>");
            }
        }
        #endregion
        //职位发布变更
        #region
        public ActionResult position_change_update()
        {
            return View();
        }
        public ActionResult position_change_Index(int currentPage)
        {
            int rows = 0;
            var list= imr.FenYe(e=>e.mre_id,e=>e.mre_id >0,out rows,currentPage,2);
            Dictionary<string, object> di = new Dictionary<string, object>();
            di.Add("rows",rows);
            di.Add("list",list);
            di.Add("pages",rows%2>0?(rows / 2)+1: (rows / 2));
            return Content(JsonConvert.SerializeObject(di));
        }
        [HttpGet]
        public ActionResult position_release_change(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<users> list1 = iub.SelectAll();
            foreach(users u in list1)
            {
                SelectListItem s = new SelectListItem()
                {
                    Text = u.u_true_name,
                    Value = u.u_true_name,
                };
                list.Add(s);
            }
            ViewData["user1"] = list;
            engage_major_release emr=imr.SelectWhere(e=>e.mre_id==id).FirstOrDefault();
            return View(emr);
        }
        [HttpPost]
        public ActionResult position_release_change(engage_major_release emr)
        {
            engage_major_release er=imr.SelectWhere(e=>e.mre_id == emr.mre_id).FirstOrDefault();
            er.human_amount = emr.human_amount;
            er.changer = emr.changer;
            er.change_time = emr.change_time;
            er.major_describe = emr.major_describe;
            er.engage_required = emr.engage_required;
            er.engage_type = emr.engage_type;
            er.deadline = emr.deadline;
            if (imr.Update(er) > 0)
            {
                return Content("<script>alert('修改成功');location.href='/position/position_change_update';</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');</script>");
            }
           
        }
        public ActionResult position_release_delete(int id)
        {
            engage_major_release emr = new engage_major_release
            {
                mre_id = (short)id,
            };
            if (imr.Delete(emr) > 0)
            {
                return Content("<script>alert('删除成功');location.href='/position/position_change_update';</script>");
            }
            else
            {
                return Content("<script>alert('删除成功');</script>");
            }
        }
        #endregion
        //职位发布查询
        #region
        public ActionResult position_release_search()
        {
            return View();
        }
        public ActionResult position_release_details(int id)
        {
            engage_major_release er = imr.SelectWhere(e => e.mre_id == id).FirstOrDefault();
            return View(er);
        }
        #endregion
    }
}