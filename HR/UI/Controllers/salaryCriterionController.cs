using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class salaryCriterionController : Controller                                         
    {
        // GET: salaryCriterion
        //薪酬标准登记
        public ActionResult salarystandard_register()
        {
            return View();
        }


    }
}