using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;
using PagedList;
using PagedList.Mvc;
namespace Project.Models
{
    public class SanPhamDao
    {
        DBShopGiayDataContext db = null;
        public SanPhamDao()
        {
            db = new DBShopGiayDataContext();
        }
        public IEnumerable<SANPHAM> ListAllPaging(int page, int pageSize)
        {
            return db.SANPHAMs.ToPagedList(page, pageSize);

        }
    }
}