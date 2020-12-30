using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        DBShopGiayDataContext db = new DBShopGiayDataContext();
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return View();
            }
            List<GioHang> listGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(listGioHang);
        }

        public ActionResult ThemGioHang(int msp, string strURL)
        {
            //Lấy giỏ hàng
            List<GioHang> listGioHang = LayGioHang();

            //Kiểm tra danh sách này có tồn tại trong session hay ko?
            GioHang SanPham = listGioHang.Find(sp => sp.iMaSP == msp);
            if (SanPham == null)
            {
                SanPham = new GioHang(msp);
                listGioHang.Add(SanPham);
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tsl = listGioHang.Sum(sp => sp.iSoLuong);
            }
            return tsl;
        }
        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                ttt += listGioHang.Sum(sp => sp.dThanhTien);
            }
            return ttt;
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }
        public ActionResult XoaGioHang(int MaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);

            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.iMaSP == MaSP);
            }

            if (lstGioHang.Count == 0)
            {
                Session["GioHang"] = null;
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang_All()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            Session["GioHang"] = null;

            return RedirectToAction("GioHang", "GioHang");
        }
        public ActionResult CapNhatGioHang(int MaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(sp => sp.iMaSP == MaSP);
            if (SanPham != null)
            {
                GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);
                sp.iSoLuong = int.Parse(f["qty"].ToString());
            }
            else
            {
                SanPham = new GioHang(MaSP);
                SanPham.iSoLuong = int.Parse(f["qty"].ToString());
                lstGioHang.Add(SanPham);
            }
            return RedirectToAction("GioHang", "GioHang");
        }
        public ActionResult DatHang()
        {
            //Kiểm tra đăng nhập
            //if (Session["taikhoan"] == null || Session["taikhoan"].ToString() == "")
            //{
            //    return RedirectToAction("DangNhap", "NguoiDung");
            //} if (Session["GioHang"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {

            //Thêm đơn hàng
            HOADON hd = new HOADON();
            //////khách hàng
            //KhachHang kh = (KhachHang)Session["taikhoan"];
            List<GioHang> gh = LayGioHang();
            //hd.IDUSER = kh.MaKH;
            hd.NGLAPHD = DateTime.Now;
            //var NgayGiao = String.Format("{0:dd/mm/yyyy}", f["Ngaygiao"]);
            //ddh.NgayGiao = DateTime.Parse(NgayGiao);
            //ddh.DaThanhToan = "Chưa Thanh Toán";
            //ddh.TinhTrangGiaoHang = 0;
            db.HOADONs.InsertOnSubmit(hd);
            db.SubmitChanges();
            foreach (var item in gh)
            {
                CTHD cthd = new CTHD();
                cthd.IDHD = hd.IDHD;
                cthd.IDSP = item.iMaSP;
                cthd.SL = item.iSoLuong;
                cthd.DONGIA = (int)item.dThanhTien;
                db.CTHDs.InsertOnSubmit(cthd);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("ThanhToan", "GioHang");
        }
        public ActionResult ThanhToan()
        {
            if (Session["LOGIN"] == null)
            {
                return RedirectToAction("Login", "Login", new { user = "-", pass = "@" });
            }
            List<GioHang> listGioHang = LayGioHang();

            //Thêm đơn hàng
            HOADON hd = new HOADON();
            //////khách hàng
            UserLogin idUser = (UserLogin)Session["LOGIN"];
            List<GioHang> gh = LayGioHang();
            hd.IDUSER = idUser.IdUser;
            hd.NGLAPHD = DateTime.Now;
            db.HOADONs.InsertOnSubmit(hd);
            db.SubmitChanges();
            foreach (var item in gh)
            {
                CTHD cthd = new CTHD();
                cthd.IDHD = hd.IDHD;
                cthd.IDSP = item.iMaSP;
                cthd.SL = item.iSoLuong;
                cthd.DONGIA = (int)item.dThanhTien;
                db.CTHDs.InsertOnSubmit(cthd);
            }
            db.SubmitChanges();
            ViewBag.MaDonHang = hd.IDHD;
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            Session["GioHang"] = null;
            return View(listGioHang);
        }
    }
}
