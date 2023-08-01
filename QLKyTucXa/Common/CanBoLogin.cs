using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKyTucXa.Common
{
    //Session tài khoản Cán bộ
    [Serializable]
    public class CanBoLogin
    {
        public long UserID { get; set; }
        public string TAIKHOAN { get; set; }
    }
}