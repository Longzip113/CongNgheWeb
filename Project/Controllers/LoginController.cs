using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
namespace Project.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        dbShopBanGiayDataContext db = new dbShopBanGiayDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(USER _user, FormCollection f)
        {

            var HOTEN = f["HOTEN"];
            var PWORD = f["PWORD"];
            var REPWORD = f["REPWORD"];
            var EMAIL = f["EMAIL"];
            var DCHI = f["DCHI"];
            var NGSINH = string.Format("{0:MM/DD/YYYY}", f["NGSINH"]);
            var SDT = f["SDT"];
            if (string.IsNullOrEmpty(HOTEN))
            {
                ViewData["loi1"] = "Họ và tên không được bỏ trống";
            }

            if (string.IsNullOrEmpty(PWORD))
            {
                ViewData["loi2"] = "vui lòng nhập mật khẩu";
            }
            if (string.IsNullOrEmpty(REPWORD))
            {
                ViewData["loi3"] = "vui lòng nhập mật khẩu";
            }
            if (string.IsNullOrEmpty(SDT))
            {
                ViewData["loi4"] = "vui lòng nhập số điện thoại";
            }
            if (string.IsNullOrEmpty(EMAIL))
            {
                ViewData["loi5"] = "Email đăng nhập không được bỏ trống";
            }
            if (!string.IsNullOrEmpty(EMAIL) && !string.IsNullOrEmpty(HOTEN) && !string.IsNullOrEmpty(PWORD) && !string.IsNullOrEmpty(REPWORD) && !string.IsNullOrEmpty(SDT) && db.USERs.Where(t => t.EMAIL == EMAIL).Count() == 0)
            {
                    _user.HOTEN = HOTEN;
                    _user.PWORD = PWORD;
                    _user.EMAIL = EMAIL;
                    _user.DCHI = DCHI;
                    _user.NGSINH = DateTime.Parse(NGSINH);
                    _user.SDT = SDT;
                    db.USERs.InsertOnSubmit(_user);
                    db.SubmitChanges();
                    return RedirectToAction("DangNhap", "Login");
            }              
            else
            {
                ViewData["loi6"] = "Email này đã đc đăng kí";
                 return View();
            }
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(USER _user, FormCollection f)
        {
            var EMAIL = f["EMAIL"];
            var PWORD = f["PWORD"];
            if (string.IsNullOrEmpty(EMAIL))
            {
                ViewData["loi1"] = "Vui lòng nhập tên đăng nhập";
            }
            if (string.IsNullOrEmpty(PWORD))
            {
                ViewData["loi2"] = "Vui lòng nhập mật khẩu";
            }
            if (!string.IsNullOrEmpty(EMAIL) && !string.IsNullOrEmpty(PWORD))
            {
                int count = db.USERs.Where(t => t.EMAIL == EMAIL).Where(mk => mk.PWORD == PWORD).Count();
                if (count == 1)
                {
                    return RedirectToAction("DanhSachGiay", "CuaHang");
                }
                if (count >= 2) {
                    ViewData["loi3"] = "Tài Khoản đã bị khóa vui lòng đăng ký tài khoản khác";
                }
                return View();
            }
            else
            {
                return View();
            }
        }

    }
}
