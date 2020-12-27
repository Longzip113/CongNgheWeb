using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CuaHangController : Controller
    {
        //
        // GET: /CuaHang/

        //public ActionResult DanhSachGiay()
        //{
        //    return View();
        //}

        public ActionResult DanhSachGiay(String id = "")
        {
            return View();
        }


        public ActionResult ChiTietSanPham()
        {
            return View();
        }

        public ActionResult TimKiem(String tenSP = "")
        {
            return View();
        }

    }
}
