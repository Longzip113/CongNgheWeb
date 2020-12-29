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
        DBShopGiayDataContext db = new DBShopGiayDataContext();
        public ActionResult Login(String user, String pass)
        {
            if (user.Equals("-"))
            {
                ViewData["loidathang"] = "Vui lòng đăng nhập trước khi đặt hàng !!!";
                return View();
            }
            if (!user.Equals("@"))
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    ViewData["loi"] = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu !!!";
                    return View();
                }

                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
                {
                    var dao = new UserDAO();
                    var result = dao.login(user, pass);

                    if (result)
                    {
                        var userr = dao.getById(user);
                        var UserSession = new UserLogin();
                        UserSession.DiaChi1 = userr.DCHI;
                        UserSession.HoTen = userr.HOTEN;
                        UserSession.SDT1 = userr.SDT;
                        UserSession.Idrole = (Int16)userr.IDROLE;
                        UserSession.IdUser = userr.IDUSER;
                        Session.Add("LOGIN", UserSession);
                        return RedirectToAction("DanhSachGiayPaging", "CuaHang", new { style = "all", item = 0, skip = 0 });
                    }
                    else
                    {
                        ViewData["loi"] = "Tên đăng nhập hoăt mật khẩu không đúng xin kiểm tra lại !!!";
                        return View();
                    }
                }
            }
            return View();
        }

        public ActionResult Logout(String user, String pass)
        {
            if (Session["LOGIN"] != null)
            {
                Session.Remove("LOGIN");
            }

            return RedirectToAction("Login", "Login", new { user = "@", pass = "@" });
        }

        public ActionResult register(String user, String pass, String hoten, String sdt, String diachi, String pass2)
        {
            
            if (!user.Equals("@"))
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(diachi))
                {
                    ViewData["lloi"] = "Vui lòng nhập đầy đủ thông tin xin cảm ơn !!!";
                    return View();
                }

                if (!pass.Equals(pass2))
                {
                    ViewData["lloi"] = "Vui lòng nhập đúng 2 mật khẩu !!!";
                    return View();
                }
                else
                {

                    var dao1 = new UserDAO();
                    var result = dao1.checkUser(user);
                    if (result)
                    {
                        ViewData["lloi"] = "Tài khoản đã tồn tại xin chọn tài khoản khác !!!";
                        return View();
                    }
                    else
                    {
                        try
                        {
                            USER entity = new USER();
                            entity.DCHI = diachi;
                            entity.HOTEN = hoten;
                            entity.PASSWORD = pass;
                            entity.IDROLE = (Int32)2;
                            entity.SDT = sdt;
                            entity.USERNAME = user;
                            var dao = new UserDAO();
                            dao.insert(entity);

                            return RedirectToAction("Login", "Login", new { user = "@", pass = "@" });
                        }
                        catch
                        {
                            ViewData["lloi"] = "Thêm thất bại vui lòng nhập đúng thông tin !!!";
                            return View();
                        }
                    }
                }
            }
            return View();
        }

        //public ActionResult DangNhap(String user, String pass)
        //{
        //    if (string.IsNullOrEmpty(user))
        //    {
        //        ViewData["loi1"] = "Vui lòng nhập tên đăng nhập";
        //    }
        //    if (string.IsNullOrEmpty(pass))
        //    {
        //        ViewData["loi2"] = "Vui lòng nhập mật khẩu";
        //    }

        //    if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
        //    {
        //        var dao = new UserDAO();
        //        var result = dao.login(user, pass);

        //        if (result)
        //        {
        //            var userr = dao.getById(user);
        //            var UserSession = new UserLogin();
        //            UserSession.DiaChi1 = userr.DCHI;
        //            UserSession.HoTen = userr.HOTEN;
        //            UserSession.SDT1 = userr.SDT;
        //            UserSession.Idrole = userr.IDROLE + "";
        //            Session.Add("LOGIN",UserSession);
        //            return RedirectToAction("DanhSachGiay","CuaHang", new { style = "all", item = 0 });
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Đăng nhập không đúng !!!");
        //        }
        //    }
        //    return RedirectToAction("Login", ViewBag);
        //}

    }
}
