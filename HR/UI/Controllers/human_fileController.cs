using DAL;
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
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class human_fileController : Controller
    {
        Ihuman_fileBll hb = IocContain.CreateAll<Ihuman_fileBll>("yibll", "human_filebll");
        Iconfig_file_first_kindBLL iffk = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        Iconfig_file_second_kindBLL ifsk = IocContain.CreateAll<Iconfig_file_second_kindBLL>("yibll", "config_file_second_kindBLL");
        Iconfig_file_third_kindBLL iftk = IocContain.CreateAll<Iconfig_file_third_kindBLL>("yibll", "config_file_third_kindBLL");
        HREntities1 hd = new HREntities1();
        // GET: human_file
        public ActionResult Index()
        {
            return View();
        }

        //查询全部
        public ActionResult Index2()
        {
            var dt = iffk.SelectAll();
            return Content(JsonConvert.SerializeObject(dt));
        }


        //查询二级菜单
        public ActionResult Index3(string id)
        {
            var dt = ifsk.SelectWhere(e=>e.first_kind_id==id);
            return Content(JsonConvert.SerializeObject(dt));
        }
        //查询三级菜单
        public ActionResult Index4()
        {
            string sid=  Request["sid"];
            string fid = Request["fid"];
            var dt = iftk.SelectWhere(e=>e.first_kind_id==fid&&e.second_kind_id==sid).Select(e => e).ToList();
            return Content(JsonConvert.SerializeObject(dt));
        }

        //查询分类
        public ActionResult Index5()
        {
            string sql = "select * from dbo.human_file";
            var dt= DBHelper.Select(sql,"");
            return Content(JsonConvert.SerializeObject(dt));
        }

        //查询分类
        public ActionResult Index6()
        {
            string qid = Request["qid"];
            string sql = string.Format("select * from dbo.human_file where human_major_kind_id='{0}'", qid);
            var dt = DBHelper.Select(sql, "");
            return Content(JsonConvert.SerializeObject(dt));
        }



    }
}