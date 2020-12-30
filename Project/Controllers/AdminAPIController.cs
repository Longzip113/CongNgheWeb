using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Models;

namespace Project.Controllers
{
    public class AdminAPIController : ApiController
    {
        public string Post(String tensp, String mota, String gia, String loai, String size, int idTH)
        {
            SANPHAM sp = new SANPHAM();
            sp.MOTA = mota;
            sp.LOAI = loai;
            sp.SIZE = int.Parse(size);
            sp.TENSP = tensp;
            sp.GIABAN = int.Parse(gia);
            sp.IDTHUONGHIEU = idTH;

            using (DBShopGiayDataContext db = new DBShopGiayDataContext())
            {
                db.SANPHAMs.InsertOnSubmit(sp);
                db.SubmitChanges();
                return "Submit Successfully ...";
            }
        }
    }
}
