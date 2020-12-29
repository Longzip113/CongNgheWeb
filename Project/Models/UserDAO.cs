using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;
using PagedList;
using PagedList.Mvc;
namespace Project.Models
{
    public class UserDAO
    {
        DBShopGiayDataContext db = null;

        public UserDAO()
        {
            db = new DBShopGiayDataContext();
        }

        public long insert(USER entity)
        {
            db.USERs.InsertOnSubmit(entity);
            db.SubmitChanges();

            return entity.IDUSER;
        }

        public USER getById(String userName)
        {
            return db.USERs.SingleOrDefault(x => x.USERNAME == userName);        
        }

        public bool login(String User, String pass)
        {
            var result = db.USERs.Count(x => x.USERNAME == User && x.PASSWORD == pass);
            if (result > 0) return true;
            else return false;
        }


        public bool checkUser(String User)
        {
            var result = db.USERs.Count(x => x.USERNAME == User);
            if (result > 0) return true;
            else return false;
        }

    }
}