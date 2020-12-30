using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Project.Models;

namespace Project
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        DBShopGiayDataContext db = new DBShopGiayDataContext();

        [WebMethod]
        public string add(SANPHAM sp)
        {
            db.SANPHAMs.InsertOnSubmit(sp);
            db.SubmitChanges();
            return "Insert thành công";
        }

        [WebMethod]
        public string edit(SANPHAM sp)
        {
            SANPHAM spedit = db.SANPHAMs.Single(s => s.IDSP == sp.IDSP);
            spedit.HINHANHSP = sp.HINHANHSP;
            spedit.TENSP = sp.TENSP;
            spedit.MOTA = sp.MOTA;
            spedit.LOAI = sp.LOAI;
            spedit.GIABAN = sp.GIABAN;
            spedit.SIZE = sp.SIZE;

            db.SubmitChanges();
            return "Edit thành công";
        }

        [WebMethod]
        public string delete(int id)
        {
            SANPHAM sp = db.SANPHAMs.Single(s => s.IDSP == id);
            db.SANPHAMs.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return "Delete thành công";
        }
    }
}
