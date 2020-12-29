using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class UserLogin
    {
        
        String hoTen;

        public String HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }
        String DiaChi;

        public String DiaChi1
        {
            get { return DiaChi; }
            set { DiaChi = value; }
        }
        String SDT;

        public String SDT1
        {
            get { return SDT; }
            set { SDT = value; }
        }

        [Required(ErrorMessage = "Nhập mật khẩu !!!")]
        String password;


        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        [Required(ErrorMessage = "Nhập tài khoản !!!")]
        String userName;

        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        int idrole;

        public int Idrole
        {
            get { return idrole; }
            set { idrole = value; }
        }

        int idUser;

        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }
    }
}