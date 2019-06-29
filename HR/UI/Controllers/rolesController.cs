using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using IBLL;
using IocContainer;
using System.Data;
using Newtonsoft.Json;
using DAL;

namespace UI.Controllers
{
    public class rolesController : Controller
    {
        UserIBLL iab = IocContain.CreateAll<UserIBLL>("yibll", "UserBll");
        RoleIBLL ril = IocContain.CreateAll<RoleIBLL>("yibll", "RoleBll");
        // GET: roles
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Index2(int currentPage, int pageSize)
        {
            int rows = 0;                                  // 一共有多少条数据      当前多少页      当前页显示几条数据
            List<Role> list = ril.FenYestandard(e => e.roleid, e => e.roleid > 0, out rows, currentPage, pageSize);
            Dictionary<string, object> msg1 = new Dictionary<string, object>();
            msg1["dt"] = list;
            msg1["row"] = rows;
            return Content(JsonConvert.SerializeObject(msg1));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Role r)
        {
            //Role re = new Role();
            //re.rolename = Request["rolename"];    通过Request取值 

            int num = ril.Add(r);
            if (num > 0)
            {
                return Content("ok");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            Role u = new Role();
            u.roleid = id;
            if (ril.Delete(u) > 0)
            {
                return Content("<script>alert('删除成功！');window.location.href='/roles/Index'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='/roles/Index'</script>");
            }
        }
        public ActionResult Edit(int id)
        {
            var ss = ril.SelectWhere(e => e.roleid == id);
            Role r = new Role
            {
                roleid = ss[0].roleid,
                rolename = ss[0].rolename,
                roleexplain = ss[0].roleexplain,
                usable = ss[0].usable

            };
            ViewData["usabid"] = ss[0].usable;

            return View(r);
        }
        [HttpPost]
        public ActionResult Edit(Role r)
        {
            if (ril.Update(r) > 0)
            {

                return Content("ok");
            }
            else
            {
                return View();
            }


        }
        //public ActionResult xg(int id)
        //{

        //    string[] sid = Request["id"].ToString().Split(',');
        //    for (int i = 0; i < sid.Length; i++)
        //    {
        //        String cid = sid[i];
        //        string sql = string.Format("insert into dbo.RoleQuan (rid,qid)values('{0}','{1}') ", id, cid);


        //    }
        //    return Content("ok");
        //}
        public ActionResult dt11(int id11)
        {
            string sql = "";
            int ss = Convert.ToInt32(Session["roid"]);
            if (Request["id"] == null)
            {
                //根节点
                sql = string.Format(@"select [id],[text],[state],case
	when qr.qid is null then 0
	else 1
 end as checked
from [dbo].[Quan] q
left join(
select qid
from dbo.RoleQuan
where rid={0}
) qr on q.id=qr.qid
where fid=0
", ss);
            }
            else
            {
                sql = string.Format(@"select [id],[text],[state],case
	when qr.qid is null then 0
	else 1
 end as checked
from [dbo].[Quan] q
left join(
select qid
from dbo.RoleQuan
where rid={0}
) qr on q.id=qr.qid
where fid={1}", id11, Request["id"]);
            }
            DataTable dt = DBHelper.MyDataAdapter(sql, "");
            return Content(JsonConvert.SerializeObject(dt));
        }
        public ActionResult shanzen(int sid,string[] qid)
        {
            string[] qwe = qid[0].ToString().Split(',');
            
            string sql1 = string.Format(@"delete from RoleQuan where rid={0}",sid);
            DBHelper.InsertUpdateDelte(sql1);
            
            for (int i = 0; i < qwe.Length; i++)
            {
                String cid = qwe[i];
                string sql = string.Format("insert into dbo.RoleQuan (rid,qid) values ('{0}','{1}') ", sid, cid);
                int tj = DBHelper.InsertUpdateDelte(sql);

            }

            return Content("ok");
        }
    }
}