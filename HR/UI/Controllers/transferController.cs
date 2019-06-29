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
    public class transferController : Controller
    {
        HREntities3 aet = new HREntities3();
        transferIBLL tib= IocContain.CreateAll<transferIBLL>("yibll", "transferBll");
        // GET: transfer
        public ActionResult Index()
        {
            List<major_change> user = tib.SelectAll();
            SelectList userlist = new SelectList(user, "first_kind_id", "first_kind_name");
            ViewData["select1"] = userlist;

            return View();
        }
    }
}