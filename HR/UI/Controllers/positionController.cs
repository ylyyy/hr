using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class positionController : Controller
    {
        // GET: position
        public ActionResult position_register()
        {
            return View();
        }
    }
}