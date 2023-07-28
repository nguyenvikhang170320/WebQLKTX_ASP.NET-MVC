using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKyTucXa.Areas.CanBo.Models
{
    public class DienNuoc
    {
        public int ID_DIEN { get; set; }
        public int ID_NUOC { get; set; }
        public string MAPHONG { get; set; }
        public string MADAYPHONG { get; set; }

        public int ID_PHONG { get; set; }

        public int? DIENCHISODAU { get; set; }

        public int? DIENCHISOCUOI { get; set; }
        public int? NUOCCHISODAU { get; set; }

        public int? NUOCCHISOCUOI { get; set; }

        public int? THANGDIEN { get; set; }

        public int? NAMDIEN { get; set; }
        public int? THANGNUOC { get; set; }

        public int? NAMNUOC { get; set; }

        public int? TRANGTHAIDIEN { get; set; }
        public int? TRANGTHAINUOC { get; set; }
    }
}