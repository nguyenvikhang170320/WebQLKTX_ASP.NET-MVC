using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKyTucXa.Areas.CanBo.Models
{
    public class HDPhong
    {
       
        public string MAPHONG { get; set; }
        public string MADAYPHONG { get; set; }

        public int ID_PHONG { get; set; }
        public int ID_HOADONPHONG { get; set; }
        public int? KY { get; set; }
        public int? NAM { get; set; }

        public bool? TRANGTHAIPHONG { get; set; }
        public int? TRANGTHAI { get; set; }
    }
}