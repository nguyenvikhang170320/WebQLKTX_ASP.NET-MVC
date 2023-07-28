using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKyTucXa.Models
{
    public class LoginPhongModel
    {
        [Required(ErrorMessage = "Mời nhập tài khoản phòng")]
        public string TAIKHOAN { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu phòng")]
        public string MATKHAU { get; set; }
    }
}