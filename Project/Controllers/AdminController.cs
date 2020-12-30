using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        DBShopGiayDataContext db = new DBShopGiayDataContext();

        public ActionResult AdminHome(String style, int item)
        {
            int idRole = (Session["LOGIN"] as UserLogin).Idrole;
            if (idRole == 2)
            {
                if (style.Equals("TheoHieu"))
                {
                    var Listgiay = db.SANPHAMs.Where(s => s.IDTHUONGHIEU == item).OrderBy(s => s.GIABAN).ToList();
                    if (Listgiay.Count == 0)
                    {
                        ViewBag.SANPHAM = "khong co sach nào thuộc thuong hiệu này !";
                    }
                    return View(Listgiay);
                }
                else if (style.Equals("Size"))
                {
                    var Listgiay = db.SANPHAMs.Where(s => s.SIZE == item).OrderBy(s => s.GIABAN).ToList();
                    if (Listgiay.Count == 0)
                    {
                        ViewBag.SANPHAM = "khong co giay nào thuộc thuong hiệu này !";
                    }
                    return View(Listgiay);
                }
                else if (style.Equals("add"))
                {
                    ViewData["Edit"] = "Bạn thêm thành công !!!";
                    var Listgiay = db.SANPHAMs.OrderBy(s => s.TENSP).ToList();
                    return View(Listgiay);
                }
                else if (style.Equals("edit"))
                {
                    ViewData["Edit"] = "Bạn edit thành công !!!";
                    var Listgiay = db.SANPHAMs.OrderBy(s => s.TENSP).ToList();
                    return View(Listgiay);
                }
                else if (style.Equals("delete"))
                {
                    ViewData["Edit"] = "Bạn Delete thành công !!!";
                    var Listgiay = db.SANPHAMs.OrderBy(s => s.TENSP).ToList();
                    return View(Listgiay);
                }
                else
                {
                    var Listgiay = db.SANPHAMs.OrderBy(s => s.TENSP).ToList();
                    return View(Listgiay);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login", new { user = "@", pass = "@" });
            }
        }

        public ActionResult AdminEdit(String style, int id)
        {
            if (style.Equals("edit"))
            {
                SANPHAM sp = db.SANPHAMs.Single(s => s.IDSP == id);
                ViewData["IDSP"] = sp.IDSP;
                ViewData["HINHANHSP"] = sp.HINHANHSP;
                ViewData["LOAI"] = sp.LOAI;
                ViewData["GIABAN"] = sp.GIABAN;
                ViewData["MOTA"] = sp.MOTA;
                ViewData["TENSP"] = sp.TENSP;
                ViewData["SIZE"] = sp.SIZE;
                ViewData["SOLUONG"] = sp.SOLUONG;
                ViewData["IDTH"] = sp.IDTHUONGHIEU;

                ViewData["STYLE"] = "Edit";
            }
            else
            {
                ViewData["STYLE"] = "Add";
            }
            return View();
        }

        

        //Load bang thuong hieu
        public ActionResult ThuongHieu()
        {

            var ListTHuongHieu = db.THUONGHIEUs.OrderBy(cd => cd.TENTHUONGHIEU).ToList();
            return View(ListTHuongHieu);
        }

        WebService obj;

        public ActionResult add(String tensp, String mota, String gia, String loai, String size, int idTH, String hinhanh, String style, int idSP)
        {
            obj = new WebService();

            SANPHAM sp = new SANPHAM();
            sp.HINHANHSP = hinhanh;
            sp.TENSP = tensp;
            sp.MOTA = mota;
            if (loai.Equals("1"))
            {
                sp.LOAI = "Nam";
            }
            else
            {
                sp.LOAI = "Nữ";
            }
            sp.GIABAN = int.Parse(gia);
            sp.SIZE = int.Parse(size);
            sp.IDTHUONGHIEU = idTH;
            sp.IDSP = idSP;
            if (style.Equals("Edit"))
            {
                obj.edit(sp);
                return RedirectToAction("AdminHome", new { style = "edit", item = -1 });
            }
            else
            {
                obj.add(sp);
                return RedirectToAction("AdminHome", new { style = "add", item = -1 });
            }
        }

        public ActionResult Delete(int id)
        {
            obj = new WebService();
            obj.delete(id);
            return RedirectToAction("AdminHome", new { style = "delete", item = -1 });
        }


    }
}
