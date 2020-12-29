using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        DBShopGiayDataContext db = new DBShopGiayDataContext();
        public ActionResult Index()
        {
            var Listgiay = db.SANPHAMs.OrderBy(s => s.TENSP).Take(8).ToList();
            return View(Listgiay);
        }

    }
}
