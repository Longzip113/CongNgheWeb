using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class GioHang
    {
        DBShopGiayDataContext db = new DBShopGiayDataContext();
        public int iMaSP { get; set; }
        public string sThuongHieu { get; set; }
        public string sTenSP { get; set; }
        public int iSize { get; set; }
        public string sLoai { get; set; }
        public double dGiaBan { get; set; }
        public int iSoLuong { get; set; }
        public string sHinhAnh { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dGiaBan; }
        }
        public GioHang(int maSP)
        {
            iMaSP = maSP;
            SANPHAM SP = db.SANPHAMs.Single(sp => sp.IDSP == iMaSP);
            int idTH = SP.IDTHUONGHIEU;
            THUONGHIEU TH = db.THUONGHIEUs.Single(th => th.IDTHUONGHIEU == idTH);
            sThuongHieu = TH.TENTHUONGHIEU;
            sTenSP = SP.TENSP;
            iSize = (int)SP.SIZE;
            sLoai = SP.LOAI;
            dGiaBan = (double)SP.GIABAN;
            sHinhAnh = SP.HINHANHSP;
            iSoLuong = 1;
        }
    }
}