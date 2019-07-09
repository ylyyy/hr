using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using DAL;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            string sql = "";
            string s = Request["id"];
            int ss= Convert.ToInt32(Session["roid"]);
            if (Request["id"] == null)
            {
                sql = string.Format(@"select * from dbo.Quan as qwe
inner join (select qid from dbo.RoleQuan where rid={0}) as tree
on tree.qid=qwe.id
where fid=0",ss);
            }
            else
            {
                sql = string.Format(@"select * from dbo.Quan as qwe
inner join (select qid from dbo.RoleQuan where rid={0}) as tree
on tree.qid=qwe.id
where fid={1}",ss,s);
            }
            DataTable dt = DBHelper.MyDataAdapter(sql, "");
            return Content(JsonConvert.SerializeObject(dt));
        }
    }
}