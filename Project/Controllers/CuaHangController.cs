using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using PagedList;
using PagedList.Mvc;
namespace Project.Controllers
{
    public class CuaHangController : Controller
    {
        //
        // GET: /CuaHang/
        DBShopGiayDataContext db = new DBShopGiayDataContext();
        //Chi tiet giay
        public ActionResult ChiTietSanPham(int ma)
        {
            SANPHAM sp = db.SANPHAMs.Single(s => s.IDSP == ma);
            var listDG = db.DANHGIAs.Where(s => s.IDSP == sp.IDSP).ToList();

            int s1 = db.DANHGIAs.Count(s => s.SOSAO == 1 && s.IDSP == sp.IDSP);
            int s2 = db.DANHGIAs.Count(s => s.SOSAO == 2 && s.IDSP == sp.IDSP);
            int s3 = db.DANHGIAs.Count(s => s.SOSAO == 3 && s.IDSP == sp.IDSP);
            int s4 = db.DANHGIAs.Count(s => s.SOSAO == 4 && s.IDSP == sp.IDSP);
            int s5 = db.DANHGIAs.Count(s => s.SOSAO == 5 && s.IDSP == sp.IDSP);

            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewData["IDSP"] = sp.IDSP;
            ViewData["HINHANHSP"] = sp.HINHANHSP;
            ViewData["LOAI"] = sp.LOAI;
            ViewData["GIABAN"] = sp.GIABAN;
            ViewData["MOTA"] = sp.MOTA;
            ViewData["TENSP"] = sp.TENSP;
            ViewData["SIZE"] = sp.SIZE;
            ViewData["SOLUONG"] = sp.SIZE;

            ViewData["s1"] = s1;
            ViewData["s2"] = s2;
            ViewData["s3"] = s3;
            ViewData["s4"] = s4;
            ViewData["s5"] = s5;

            int tong = s5 + s4 + s3 + s2 + s1;
            ViewData["TongNhanXet"] = tong;
            if (tong != 0)
            {
                int NhanXetTong = (s5 * 5 + s4 * 4 + s3 * 3 + s2 * 2 + s1 * 1) / tong;
                ViewData["NhanXetTong"] = NhanXetTong;
            }
            else
            {
                ViewData["NhanXetTong"] = 0;
            }
            return View(listDG);
        }

        //Load bang thuong hieu
        public ActionResult ThuongHieu()
        {

            var ListTHuongHieu = db.THUONGHIEUs.OrderBy(cd => cd.TENTHUONGHIEU).ToList();
            return View(ListTHuongHieu);
        }

        //Load giayTheoStyle
        //public ActionResult DanhSachGiay(String style, int item)
        //{
        //    int SoGiay;
        //    int pageLimit = 9;
        //    int paging;
        //    if (style.Equals("Nam") || style.Equals("Nữ"))
        //    {
        //        SoGiay = db.SANPHAMs.Where(s => s.LOAI == style).Count();
        //    }
        //    else
        //    {
        //        SoGiay = db.SANPHAMs.Count();
        //    }
        //    if ((float)SoGiay / pageLimit > SoGiay / pageLimit)
        //    {
        //        paging = (SoGiay / pageLimit) + 1;
        //    }
        //    else
        //    {
        //        paging = (SoGiay / pageLimit);
        //    }
        //    ViewBag.paging = paging;
        //    ViewBag.style = style;
        //    ViewBag.style = style;
        //    if (style.Equals("TheoHieu"))
        //    {
        //        var Listgiay = db.SANPHAMs.Where(s => s.IDTHUONGHIEU == item).OrderBy(s => s.GIABAN).ToList();
        //        if (Listgiay.Count == 0)
        //        {
        //            ViewBag.SANPHAM = "khong co sach nào thuộc thuong hiệu này !";
        //        }
        //        return View(Listgiay);
        //    }
        //    else if (style.Equals("Size"))
        //    {
        //        var Listgiay = db.SANPHAMs.Where(s => s.SIZE == item).OrderBy(s => s.GIABAN).ToList();
        //        if (Listgiay.Count == 0)
        //        {
        //            ViewBag.SANPHAM = "khong co giay nào thuộc thuong hiệu này !";
        //        }
        //        return View(Listgiay);
        //    }
        //    else if (style.Equals("all"))
        //    {
        //        var Listgiay = db.SANPHAMs.OrderBy(s => s.TENSP).ToList();

        //        return View(Listgiay);
        //    }
        //    else if (item == -1)
        //    {
        //        var Listgiay = db.SANPHAMs.Where(s => s.TENSP.Contains(style)).OrderBy(s => s.GIABAN).ToList();
        //        if (Listgiay.Count == 0)
        //        {
        //            ViewBag.SANPHAM = "khong co giay nào thuộc tên này !";
        //        }
        //        return View(Listgiay);
        //    }
        //    else if (style.Equals("Gia"))
        //    {
        //        if (item == 1)
        //        {
        //            var Listgiay = db.SANPHAMs.Where(s => s.GIABAN <= 100000).OrderBy(s => s.GIABAN).ToList();
        //            return View(Listgiay);
        //        }
        //        else if (item == 2)
        //        {
        //            var Listgiay = db.SANPHAMs.Where(s => s.GIABAN >= 100000).Where(s => s.GIABAN <= 500000).OrderBy(s => s.GIABAN).ToList();
        //            return View(Listgiay);
        //        }
        //        else
        //        {
        //            var Listgiay = db.SANPHAMs.Where(s => s.GIABAN >= 500000).OrderBy(s => s.GIABAN).ToList();
        //            return View(Listgiay);
        //        }

        //    }
        //    else
        //    {
        //        var Listgiay = db.SANPHAMs.Where(s => s.LOAI == style).OrderBy(s => s.GIABAN).ToList();
        //        if (Listgiay.Count == 0)
        //        {
        //            ViewBag.SANPHAM = "khong co giay nào thuộc loại này !";
        //        }
        //        return View(Listgiay);
        //    }
        //}

        public ActionResult DanhSachGiayPaging(String style, int item, int skip)
        {
            int SoGiay; 
            int pageLimit = 9;
            int paging;
            if (style.Equals("Nam") || style.Equals("Nữ"))
            {
                SoGiay = db.SANPHAMs.Where(s => s.LOAI == style).Count();
            }
            else { 
                SoGiay = db.SANPHAMs.Count();
            }
            if ((float)SoGiay / pageLimit > SoGiay / pageLimit)
            {
                paging = (SoGiay / pageLimit) +1;
            }
            else {
                paging = (SoGiay / pageLimit);
            }
            ViewBag.paging = paging;
            ViewBag.style = style;
            if (skip == null)
            {
                skip = 0;
            }
            if (style.Equals("TheoHieu"))
            {
                var Listgiay = db.SANPHAMs.Where(s => s.IDTHUONGHIEU == item).Skip(skip).Take(pageLimit).OrderBy(s => s.GIABAN).ToList();
                if (Listgiay.Count == 0)
                {
                    ViewBag.SANPHAM = "khong co sach nào thuộc thuong hiệu này !";
                }
                return View(Listgiay);
            }
            else if (style.Equals("Size"))
            {
                var Listgiay = db.SANPHAMs.Where(s => s.SIZE == item).Skip(skip).Take(pageLimit).OrderBy(s => s.GIABAN).ToList();
                if (Listgiay.Count == 0)
                {
                    ViewBag.SANPHAM = "khong co giay nào thuộc thuong hiệu này !";
                }
                return View(Listgiay);
            }
            else if (style.Equals("all"))
            {
                var Listgiay = db.SANPHAMs.Skip(skip).Take(pageLimit).OrderBy(s => s.TENSP).ToList();

                return View(Listgiay);
            }
            else if (item == -1)
            {
                var Listgiay = db.SANPHAMs.Where(s => s.TENSP.Contains(style)).Skip(skip).Take(pageLimit).OrderBy(s => s.GIABAN).ToList();
                if (Listgiay.Count == 0)
                {
                    ViewBag.SANPHAM = "khong co giay nào thuộc tên này !";
                }
                return View(Listgiay);
            }
            else if (style.Equals("Gia"))
            {
                if (item == 1)
                {
                    var Listgiay = db.SANPHAMs.Where(s => s.GIABAN <= 100000).Skip(skip).Take(pageLimit).OrderBy(s => s.GIABAN).ToList();
                    return View(Listgiay);
                }
                else if (item == 2)
                {
                    var Listgiay = db.SANPHAMs.Where(s => s.GIABAN >= 100000).Skip(skip).Take(pageLimit).Where(s => s.GIABAN <= 500000).OrderBy(s => s.GIABAN).ToList();
                    return View(Listgiay);
                }
                else
                {
                    var Listgiay = db.SANPHAMs.Where(s => s.GIABAN >= 500000).Skip(skip).Take(pageLimit).OrderBy(s => s.GIABAN).ToList();
                    return View(Listgiay);
                }

            }
            else
            {
                var Listgiay = db.SANPHAMs.Where(s => s.LOAI == style).Skip(skip).Take(pageLimit).OrderBy(s => s.GIABAN).ToList();
                if (Listgiay.Count == 0)
                {
                    ViewBag.SANPHAM = "khong co giay nào thuộc loại này !";
                }
                return View(Listgiay);
            }
        }

        //public ViewResult GiayTheoHieu(int ma)
        //{
        //    var Listgiay = db.SANPHAMs.Where(s => s.IDTHUONGHIEU == ma).OrderBy(s => s.GIABAN).ToList();
        //    if (Listgiay.Count == 0)
        //    {
        //        ViewBag.SANPHAM = "khong co sach nào thuộc thuong hiệu này !";
        //    }
        //    return View(Listgiay);
        //}

        //public ViewResult GiaySize(int size)
        //{
        //    var Listgiay = db.SANPHAMs.Where(s => s.SIZE == size).OrderBy(s => s.GIABAN).ToList();
        //    if (Listgiay.Count == 0)
        //    {
        //        ViewBag.SANPHAM = "khong co giay nào thuộc thuong hiệu này !";
        //    }
        //    return View(Listgiay);
        //}

        //public ViewResult GiayLoai(string loai)
        //{
        //    var Listgiay = db.SANPHAMs.Where(s => s.LOAI == loai).OrderBy(s => s.GIABAN).ToList();
        //    if (Listgiay.Count == 0)
        //    {
        //        ViewBag.SANPHAM = "khong co giay nào thuộc loại này !";
        //    }
        //    return View(Listgiay);
        //}

        public ActionResult TimKiem(string name)
        {
            return RedirectToAction("DanhSachGiayPaging", new { style = name, item = -1, skip=0 });
        }

        public ActionResult DanhGia(int idSP, int soSao, String content)
        {
            if (!(Session["LOGIN"] == null))
            {
                DANHGIA dg = new DANHGIA();
                //////khách hàng
                UserLogin idUser = (UserLogin)Session["LOGIN"];
                dg.IDUSER = idUser.IdUser;
                dg.IDSP = idSP;
                dg.NOTE = content;
                dg.SOSAO = soSao;
                db.DANHGIAs.InsertOnSubmit(dg);
                db.SubmitChanges();
                return RedirectToAction("ChiTietSanPham", new { ma = idSP });
            }
            else
            {
                return RedirectToAction("Login", "Login", new { user = "@", pass = "@" });
            }
        }



    }
}
