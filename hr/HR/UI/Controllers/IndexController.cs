using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult left()
        {
            return View();
        }
        public ActionResult main()
        {
            return View();
        }
        public ActionResult top()
        {
            return View();
        }
    }
}