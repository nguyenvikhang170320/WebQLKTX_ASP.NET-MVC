using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKyTucXa.Areas.CanBo.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập tài khoản cán bộ")]
        public string TAIKHOAN { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu cán bộ")]
        public string MATKHAU { get; set; }
    }
}