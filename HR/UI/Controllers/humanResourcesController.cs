using Entity;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class humanResourcesController : Controller
    {
        // GET: humanResources
        Iconfig_file_first_kindBLL iffk = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        Iconfig_file_second_kindBLL ifsk = IocContain.CreateAll<Iconfig_file_second_kindBLL>("yibll", "config_file_second_kindBLL");
        Iconfig_file_third_kindBLL iftk = IocContain.CreateAll<Iconfig_file_third_kindBLL>("yibll", "config_file_third_kindBLL");
        Iconfig_public_charBLL ipcc = IocContain.CreateAll<Iconfig_public_charBLL>("yibll", "config_public_charBLL");//公共字段设置
        Iconfig_major_kindBLL ifmk = IocContain.CreateAll<Iconfig_major_kindBLL>("yibll", "config_major_kindBLL");//职位分类设置
        Iconfig_majorBLL ifm = IocContain.CreateAll<Iconfig_majorBLL>("yibll", "config_majorBLL");//职位设置
        Ihuman_fileBll human = IocContain.CreateAll<Ihuman_fileBll>("yibll", "humanbll");
        ISalary_strandardBll issbll = IocContain.CreateAll<ISalary_strandardBll>("yibll", "ssarybll");//薪酬标准
        Iengage_resumeBLL irb = IocContain.CreateAll<Iengage_resumeBLL>("yibll", "engage_resumeBLL");
        Iengage_interviewBLL ib = IocContain.CreateAll<Iengage_interviewBLL>("yibll", "engage_interviewBLL");
        
        public ActionResult human_registerIndex()
        {
            return View();
        }

        //人力资源档案登记 显示
        [HttpGet]
        public ActionResult human_register(int id)
        {
            Session["LuYong"] = id;
            ViewData["first"] = iffk.SelectAll();
            ViewData["major_kind"] = ifmk.SelectAll();
            ViewData["designation"] = ipcc.SelectWhere(e => e.attribute_kind == "职称");
            ViewData["mingzu"] = ipcc.SelectWhere(e => e.attribute_kind == "民族");
            ViewData["guoji"] = ipcc.SelectWhere(e => e.attribute_kind == "国籍");
            ViewData["zongj"] = ipcc.SelectWhere(e => e.attribute_kind == "宗教信仰");
            ViewData["zzmmao"] = ipcc.SelectWhere(e => e.attribute_kind == "政治面貌");
            ViewData["xueli"] = ipcc.SelectWhere(e => e.attribute_kind == "学历");
            ViewData["jyuage"] = ipcc.SelectWhere(e => e.attribute_kind == "教育年限");
            ViewData["zhuanye"] = ipcc.SelectWhere(e => e.attribute_kind == "专业");
            ViewData["techang"] = ipcc.SelectWhere(e => e.attribute_kind == "特长");
            ViewData["aihao"] = ipcc.SelectWhere(e => e.attribute_kind == "爱好");
            ViewData["salarystrandard"] = issbll.SelectWhere(e => e.check_status == 1);
            ViewData["engage_resume"] = irb.SelectWhere(e => e.res_id == id).FirstOrDefault();
            return View();
        }
        //人力资源档案登记 几级
        [HttpPost]
        public ActionResult human_register(string jigou, string id)
        {
            if (jigou == "1")
            {
                return Content(JsonConvert.SerializeObject(ifsk.SelectWhere(e => e.first_kind_id == id)));
            }
            else if (jigou == "2")
            {
                return Content(JsonConvert.SerializeObject(iftk.SelectWhere(e => e.second_kind_id == id)));
            }
            else if (jigou == "majork")
            {
                return Content(JsonConvert.SerializeObject(ifm.SelectWhere(e => e.major_kind_id == id)));
            }
            return View();
        }
        //人力资源档案登记 文件上传图片
        [HttpPost]
        public ActionResult register_choose_picture(human_file hm, string startDate)
        {
            if (hm.first_kind_id==null)
            {
                hm.first_kind_id = "";
            }
            if (hm.second_kind_id==null)
            {
                hm.second_kind_id = "";
            }
            if (hm.third_kind_id==null)
            {
                hm.third_kind_id = "";
            }
            if (hm.human_major_kind_id==null)
            {
                hm.human_major_kind_id = "";
            }
            if (hm.human_major_id==null)
            {
                hm.human_major_id = "";
            }
            if (startDate != "")
            {
                hm.human_birthday = Convert.ToDateTime(startDate);
            }
            string hmid = human.hmbianHao();
            hm.human_id = hmid;
            hm.check_status = 0;
            hm.human_file_status = true;
            hm.regist_time = DateTime.Now;
            if (hm.salary_standard_id!="")
            {
                salary_standard stan = issbll.SelectWhere(e => e.standard_id == hm.salary_standard_id).FirstOrDefault();
                hm.salary_sum = stan.salary_sum;
                hm.demand_salaray_sum = stan.salary_sum;
                hm.paid_salary_sum = stan.salary_sum - (stan.salary_sum * Convert.ToDecimal(0.05));
            }
            hm.major_change_amount = 0;
            hm.bonus_amount = 0;
            hm.training_amount = 0;
            hm.file_chang_amount = 0;
            if (human.Add(hm) > 0)
            {
                ViewData["hmid"] = hmid;
                int id = Convert.ToInt32(Session["LuYong"]);
                engage_resume er = new engage_resume()
                {
                    res_id = (short)id,
                };
                irb.Delete(er);
                return View();
            }
            else
            {
                return Content("<script>alert('添加失败！');location.href='/humanResources/human_register';</script>");
            }
        }
        //人力资源档案登记 上传页面
        [HttpGet]
        public ActionResult register_choose_picture(string humanId)
        {
            ViewData["hmid"] = humanId;
            return View();
        }
        //人力资源档案登记 执行文件上传图片
        [HttpPost]
        public ActionResult success(string humanId,string yemian)
        {
            human_file hm = human.SelectWhere(e => e.human_id == humanId)[0];
            if (yemian=="dji")
            {
                yemian = "register_choose_picture";
            }
            else if (yemian=="fuhe")
            {
                yemian = "register_choose_picture_fuhe";
            }
            else if (yemian=="bgen")
            {
                yemian = "register_choose_picture_bgen";
            }
            HttpPostedFileBase filename = Request.Files["picFile"];
            HttpPostedFileBase pathname = Request.Files["accFile"];
            string FullName = filename.FileName;
            string PathName = pathname.FileName;
            if (FullName != ""&&PathName=="")
            {
                if (!pic(filename, FullName))
                {
                    ViewData["hmid"] = humanId;
                    return Content("<script>alert('文件类型错误！');location.href='/humanResources/"+yemian+"?humanId=" + humanId+"';</script>");
                }
            }
            else if (PathName != ""&&FullName=="")
            {
                if (!acc(pathname, PathName))
                {
                    ViewData["hmid"] = humanId;
                    return Content("<script>alert('文件类型错误！');location.href='/humanResources/"+yemian+"?humanId=" + humanId + "';</script>");
                }
            }
            else if (FullName != "" && PathName != "")
            {
                if (!pic(filename, FullName)||!acc(pathname, PathName))
                {
                    ViewData["hmid"] = humanId;
                    return Content("<script>alert('文件类型错误！');location.href='/humanResources/"+yemian+"?humanId=" + humanId + "';</script>");
                }
            }
            if (FullName!="")
            {
                hm.human_picture = FullName;
            }
            if (PathName!="")
            {
                hm.attachment_name = PathName;
            }
            if (human.Update(hm) > 0)
            {
                return View();
            }
            else {
                return Content("<script>alert('文件上传失败！');location.href='/humanResources/human_register';</script>");
            }
        }
        //附件上传
        private bool acc(HttpPostedFileBase filename, string PathName)
        {
            bool bol = false;
            FileInfo fi = new FileInfo(PathName);
            string name = fi.Name;//获取附件名称
            string type = fi.Extension;//获取附件类型
            if (type == ".doc" || type == ".txt" || type == ".jpg" || type == ".pdf")
            {
                bol = true;
                string path = Server.MapPath("~/images/" + PathName);//附件保存到文件夹下
                filename.SaveAs(path);//保存附件至该路径路径
            }
            return bol;
        }
        //图片上传
        private bool pic(HttpPostedFileBase filename, string FullName)
        {
            bool bol = false;
            FileInfo fi = new FileInfo(FullName);
            string name = fi.Name;//获取图片名称
            string type = fi.Extension;//获取图片类型
            if (type == ".jpg" || type == ".gif")
            {
                bol = true;
                string path = Server.MapPath("~/images/" + FullName);//图片保存到文件夹下
                filename.SaveAs(path);//保存图片至该路径路径
            }
            return bol;
        }
        //文件上传成功
        [HttpGet]
        public ActionResult success()
        {
            return View();
        }
        //人力资源档案登记复核   
        public ActionResult check_list() {
            ViewData["hm"] = human.SelectWhere(e=>e.check_status==0);
            return View();
        }
        //人力资源档案登记复核 复核显示
        public ActionResult human_check(string hmid) {
            ViewData["hm"] = human.SelectWhere(e=>e.human_id==hmid).FirstOrDefault();
            ViewData["designation"] = ipcc.SelectWhere(e => e.attribute_kind == "职称");
            ViewData["mingzu"] = ipcc.SelectWhere(e => e.attribute_kind == "民族");
            ViewData["guoji"] = ipcc.SelectWhere(e => e.attribute_kind == "国籍");
            ViewData["zongj"] = ipcc.SelectWhere(e => e.attribute_kind == "宗教信仰");
            ViewData["zzmmao"] = ipcc.SelectWhere(e => e.attribute_kind == "政治面貌");
            ViewData["xueli"] = ipcc.SelectWhere(e => e.attribute_kind == "学历");
            ViewData["jyuage"] = ipcc.SelectWhere(e => e.attribute_kind == "教育年限");
            ViewData["zhuanye"] = ipcc.SelectWhere(e => e.attribute_kind == "专业");
            ViewData["techang"] = ipcc.SelectWhere(e => e.attribute_kind == "特长");
            ViewData["aihao"] = ipcc.SelectWhere(e => e.attribute_kind == "爱好");
            ViewData["salarystrandard"] = issbll.SelectWhere(e => e.check_status == 1);
            return View();
        }
        //人力资源档案登记复核 复核中
        [HttpPost]
        public ActionResult register_choose_picture_fuhe(human_file hm, string startDate) {
            //hm.salary_sum =Convert.ToDecimal(Request.Form["isalary_sum"]);
            //hm.demand_salaray_sum =Convert.ToDecimal(Request.Form["idemand_salaray_sum"]);
            //hm.paid_salary_sum = Convert.ToDecimal(Request.Form["ipaid_salary_sum"]);
            int majorca = Convert.ToInt32(Request.Form["imajor_change_amount"]);
            hm.major_change_amount = (short)majorca;
            int bonusa = Convert.ToInt32(Request.Form["ibonus_amount"]);
            hm.bonus_amount = (short)bonusa;
            int traina = Convert.ToInt32(Request.Form["itraining_amount"]);
            hm.training_amount = (short)traina;
            int fileca = Convert.ToInt32(Request.Form["ifile_chang_amount"]);
            hm.file_chang_amount = (short)fileca;
            if (Request.Form["icheck_time"]!="")
            {
                hm.check_time = Convert.ToDateTime(Request.Form["icheck_time"]);
            }
            if (Request.Form["ilastly_change_time"]!="")
            {
                hm.lastly_change_time =Convert.ToDateTime(Request.Form["ilastly_change_time"]);
            }
            if (Request.Form["idelete_time"]!="")
            {
                hm.delete_time = Convert.ToDateTime(Request.Form["idelete_time"]);
            }
            if (Request.Form["irecovery_time"]!="")
            {
                hm.recovery_time = Convert.ToDateTime(Request.Form["irecovery_time"]);
            }
            if (hm.salary_standard_id != "")
            {
                salary_standard stan = issbll.SelectWhere(e => e.standard_id == hm.salary_standard_id).FirstOrDefault();
                hm.salary_sum = stan.salary_sum;
                hm.demand_salaray_sum = stan.salary_sum;
                hm.paid_salary_sum = stan.salary_sum - (stan.salary_sum * Convert.ToDecimal(0.05));
            }
            hm.register = Request.Form["iregister"];
            if (startDate != "")
            {
                hm.human_birthday = Convert.ToDateTime(startDate);
            }
            hm.human_file_status = true;
            hm.check_status = 1;
            if (human.Update(hm)>0)
            {
                ViewData["hmid"] = hm.human_id;
                return View();
            }
            else
            {
                return Content("<script>alert('复核失败！');location.href='/humanResources/check_list';</script>");
            }
        }
        //人力资源档案登记复核 复核文件上传
        [HttpGet]
        public ActionResult register_choose_picture_fuhe(string humanId) {
            ViewData["hmid"] = humanId;
            return View();
        }
        //人力资源档案查询  条件查询
        public ActionResult query_locate() {
            ViewData["first"]=iffk.SelectAll();
            ViewData["major_kind"] = ifmk.SelectAll();
            return View();
        }
        //人力资源档案查询  显示
        [HttpGet]
        public ActionResult query_list(string firstKindId,string secondKindId,string thirdKindId,string humanMajorKindId,string humanMajorId,string startDate,string endDate) {
            ViewData["firstKindId"] = firstKindId;
            ViewData["secondKindId"] = secondKindId;
            ViewData["thirdKindId"] = thirdKindId;
            ViewData["humanMajorKindId"] = humanMajorKindId;
            ViewData["humanMajorId"] = humanMajorId;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            return View();
        }
        //查询分页
        private string getList(Expression<Func<human_file, bool>> where, int currentPage)
        {
            int rows = 0;
            List<human_file> list = human.FenYe(where, out rows, currentPage, 10);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            int pages = rows % 10 == 0 ? rows / 10 : rows / 10 + 1;
            dic.Add("rows", rows);
            dic.Add("pages", pages);
            dic.Add("currentpage", currentPage);
            dic.Add("list", list);
            return JsonConvert.SerializeObject(dic);
        }
        //人力资源档案查询 显示
        [HttpPost]
        public ActionResult query_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate,int currentpage) {
            if (startDate!=""&&endDate=="")
            {
                DateTime start = Convert.ToDateTime(startDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time>=start && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            else if (startDate==""&&endDate!="")
            {
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time <=end && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            else if (startDate!=""&&endDate!="")
            {
                DateTime start = Convert.ToDateTime(startDate);
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId)&&e.regist_time>=start && e.regist_time <= end && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId)&&e.check_status==1 && e.human_file_status == true, currentpage));
        }
        //人力资源档案查询  详情
        public ActionResult query_list_information(string hmid) {
            ViewData["human"] = human.SelectWhere(e=>e.human_id==hmid).FirstOrDefault();
            return View();
        }
        //人力资源档案变更
        public ActionResult change_locate() {
            ViewData["first"] = iffk.SelectAll();
            ViewData["major_kind"] = ifmk.SelectAll();
            return View();
        }

        //人力资源档案变更 显示
        [HttpGet]
        public ActionResult change_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate) {
            ViewData["firstKindId"] = firstKindId;
            ViewData["secondKindId"] = secondKindId;
            ViewData["thirdKindId"] = thirdKindId;
            ViewData["humanMajorKindId"] = humanMajorKindId;
            ViewData["humanMajorId"] = humanMajorId;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            return View();
        }
        //人力资源档案变更 显示
        [HttpPost]
        public ActionResult change_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate, int currentpage)
        {
            if (startDate != "" && endDate == "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time >= start && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            else if (startDate == "" && endDate != "")
            {
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time <= end && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            else if (startDate != "" && endDate != "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time >= start && e.regist_time <= end && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.check_status == 1 && e.human_file_status == true, currentpage));
        }
        //人力资源档案变更 详情
        public ActionResult change_list_information(string hmid) {
            ViewData["hm"] = human.SelectWhere(e => e.human_id == hmid).FirstOrDefault();
            ViewData["designation"] = ipcc.SelectWhere(e => e.attribute_kind == "职称");
            ViewData["mingzu"] = ipcc.SelectWhere(e => e.attribute_kind == "民族");
            ViewData["guoji"] = ipcc.SelectWhere(e => e.attribute_kind == "国籍");
            ViewData["zongj"] = ipcc.SelectWhere(e => e.attribute_kind == "宗教信仰");
            ViewData["zzmmao"] = ipcc.SelectWhere(e => e.attribute_kind == "政治面貌");
            ViewData["xueli"] = ipcc.SelectWhere(e => e.attribute_kind == "学历");
            ViewData["jyuage"] = ipcc.SelectWhere(e => e.attribute_kind == "教育年限");
            ViewData["zhuanye"] = ipcc.SelectWhere(e => e.attribute_kind == "专业");
            ViewData["techang"] = ipcc.SelectWhere(e => e.attribute_kind == "特长");
            ViewData["aihao"] = ipcc.SelectWhere(e => e.attribute_kind == "爱好");
            ViewData["salarystrandard"] = issbll.SelectWhere(e => e.check_status == 1);
            return View();
        }
        //人力资源档案变更 变更文件上传
        [HttpGet]
        public ActionResult register_choose_picture_bgen(string humanId) {
            ViewData["hmid"] = humanId;
            return View();
        }
        //人力资源档案变更 变更中
        [HttpPost]
        public ActionResult register_choose_picture_bgen(human_file hm, string startDate)
        {
            //hm.salary_sum = Convert.ToDecimal(Request.Form["isalary_sum"]);
            //hm.demand_salaray_sum = Convert.ToDecimal(Request.Form["idemand_salaray_sum"]);
            //hm.paid_salary_sum = Convert.ToDecimal(Request.Form["ipaid_salary_sum"]);
            int majorca = Convert.ToInt32(Request.Form["imajor_change_amount"]);
            hm.major_change_amount = (short)majorca;
            int bonusa = Convert.ToInt32(Request.Form["ibonus_amount"]);
            hm.bonus_amount = (short)bonusa;
            int traina = Convert.ToInt32(Request.Form["itraining_amount"]);
            hm.training_amount = (short)traina;
            int fileca = Convert.ToInt32(Request.Form["ifile_chang_amount"]);
            hm.file_chang_amount = (short)fileca;
            if (Request.Form["icheck_time"] != "")
            {
                hm.check_time = Convert.ToDateTime(Request.Form["icheck_time"]);
            }
            if (Request.Form["ilastly_change_time"] != "")
            {
                hm.lastly_change_time = Convert.ToDateTime(Request.Form["ilastly_change_time"]);
            }
            if (Request.Form["idelete_time"] != "")
            {
                hm.delete_time = Convert.ToDateTime(Request.Form["idelete_time"]);
            }
            if (Request.Form["irecovery_time"] != "")
            {
                hm.recovery_time = Convert.ToDateTime(Request.Form["irecovery_time"]);
            }
            hm.register = Request.Form["iregister"];
            if (startDate != "")
            {
                hm.human_birthday = Convert.ToDateTime(startDate);
            }
            if (hm.salary_standard_id != "")
            {
                salary_standard stan = issbll.SelectWhere(e => e.standard_id == hm.salary_standard_id).FirstOrDefault();
                hm.salary_sum = stan.salary_sum;
                hm.demand_salaray_sum = stan.salary_sum;
                hm.paid_salary_sum = stan.salary_sum - (stan.salary_sum * Convert.ToDecimal(0.05));
            }
            hm.human_file_status = true;
            hm.check_status = 0;
            hm.lastly_change_time = hm.change_time;
            if (human.Update(hm) > 0)
            {
                ViewData["hmid"] = hm.human_id;
                return View();
            }
            else
            {
                return Content("<script>alert('变更失败！');location.href='/humanResources/check_list';</script>");
            }
        }
        //人力资源档案删除
        [HttpGet]
        public ActionResult delete_locate()
        {
            ViewData["first"] = iffk.SelectAll();
            ViewData["major_kind"] = ifmk.SelectAll();
            return View();
        }
        //人力资源档案删除 条件显示
        [HttpGet]
        public ActionResult delete_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate) {
            ViewData["firstKindId"] = firstKindId;
            ViewData["secondKindId"] = secondKindId;
            ViewData["thirdKindId"] = thirdKindId;
            ViewData["humanMajorKindId"] = humanMajorKindId;
            ViewData["humanMajorId"] = humanMajorId;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            return View();
        }
        //人力资源档案删除 显示数据
        [HttpPost]
        public ActionResult delete_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate, int currentpage) {
            if (startDate != "" && endDate == "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time >= start && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            else if (startDate == "" && endDate != "")
            {
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time <= end && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            else if (startDate != "" && endDate != "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time >= start && e.regist_time <= end && e.check_status == 1 && e.human_file_status == true, currentpage));
            }
            return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.check_status == 1&&e.human_file_status==true, currentpage));
        }
        //人力资源档案删除 详情
        public ActionResult delete_list_information(string hmid) {
            ViewData["human"] = human.SelectWhere(e => e.human_id == hmid).FirstOrDefault();
            return View();
        }
        //人力资源档案删除 删除
        [HttpPost]
        public ActionResult success_sc(human_file hm,string startDate) {
            if (startDate != "")
            {
                hm.human_birthday = Convert.ToDateTime(startDate);
            }
            hm.delete_time = DateTime.Now;
            hm.human_file_status = false;
            if (human.Update(hm) > 0)
            {
                return Content("<script>location.href='/humanResources/success';</script>");
            }
            else {
                return Content("<script>alert('删除失败！');location.href='/humanResources/delete_locate';</script>");
            }
        }
        //人力资源档案恢复
        public ActionResult recovery_locate() {
            ViewData["first"] = iffk.SelectAll();
            ViewData["major_kind"] = ifmk.SelectAll();
            return View();
        }
        //人力资源档案恢复 条件显示
        [HttpGet]
        public ActionResult recovery_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate) {
            ViewData["firstKindId"] = firstKindId;
            ViewData["secondKindId"] = secondKindId;
            ViewData["thirdKindId"] = thirdKindId;
            ViewData["humanMajorKindId"] = humanMajorKindId;
            ViewData["humanMajorId"] = humanMajorId;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            return View();
        }
        //人力资源档案恢复 显示数据
        [HttpPost]
        public ActionResult recovery_list(string firstKindId, string secondKindId, string thirdKindId, string humanMajorKindId, string humanMajorId, string startDate, string endDate, int currentpage)
        {
            if (startDate != "" && endDate == "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time >= start && e.check_status == 1 && e.human_file_status == false, currentpage));
            }
            else if (startDate == "" && endDate != "")
            {
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time <= end && e.check_status == 1 && e.human_file_status == false, currentpage));
            }
            else if (startDate != "" && endDate != "")
            {
                DateTime start = Convert.ToDateTime(startDate);
                DateTime end = Convert.ToDateTime(endDate);
                return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.regist_time >= start && e.regist_time <= end && e.check_status == 1 && e.human_file_status == false, currentpage));
            }
            return Content(getList(e => e.first_kind_id.Contains(firstKindId) && e.second_kind_id.Contains(secondKindId) && e.third_kind_id.Contains(thirdKindId) && e.human_major_kind_id.Contains(humanMajorKindId) && e.human_major_id.Contains(humanMajorId) && e.check_status == 1 && e.human_file_status == false, currentpage));
        }
        //人力资源档案恢复 详情
        public ActionResult recovery_list_information(string hmid) {
            ViewData["human"] = human.SelectWhere(e => e.human_id == hmid).FirstOrDefault();
            return View();
        }
        //人力资源档案恢复 恢复
        [HttpPost]
        public ActionResult success_hf(human_file hm, string startDate)
        {
            if (startDate != "")
            {
                hm.human_birthday = Convert.ToDateTime(startDate);
            }
            hm.human_file_status = true;
            if (human.Update(hm) > 0)
            {
                return Content("<script>location.href='/humanResources/success';</script>");
            }
            else
            {
                return Content("<script>alert('恢复失败！');location.href='/humanResources/delete_locate';</script>");
            }
        }
        //人力资源档案永久删除 页面显示
        [HttpGet]
        public ActionResult delete_forever_list() {
            return View();
        }
        //人力资源档案永久删除 显示数据
        [HttpPost]
        public ActionResult delete_forever_list(int currentpage) {
            return Content(getList(e=>e.check_status == 1,currentpage));
        }
        //人力资源档案永久删除 永久删除
        public ActionResult success_yjsc(int hufId) {
            human_file hm = new human_file() {
                huf_id=(short)hufId
            };
            if (human.Delete(hm) > 0)
            {
                return Content("<script>alert('删除成功！');location.href='/humanResources/delete_forever_list';</script>");
            }
            else {
                return Content("<script>alert('删除失败！');location.href='/humanResources/delete_forever_list';</script>");
            }
        }
    }
}