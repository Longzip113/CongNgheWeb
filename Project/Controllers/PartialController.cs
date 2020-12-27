using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class PartialController : Controller
    {
        //
        // GET: /Partial/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuPartial()
        {
            return PartialView();
        }

    }
}
