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
    public class clientController : Controller
    {
        Iconfig_file_first_kindBLL iffk = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        // GET: client
        public ActionResult first_kind()
        {
            return View();
        }
        public ActionResult first_kindIndex()
        {
            return Content(JsonConvert.SerializeObject(iffk.SelectAll()));
        }
        // GET: client/Create
        public ActionResult first_kindCreate()
        {
            return View();
        }

        // POST: client/Create
        [HttpPost]
        public ActionResult first_kindCreate(config_file_first_kind collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: client/Edit/5
        public ActionResult first_kindEdit(int id)
        {
            return View();
        }

        // POST: client/Edit/5
        [HttpPost]
        public ActionResult first_kindEdit(Iconfig_file_first_kindBLL collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult first_kindDelete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
