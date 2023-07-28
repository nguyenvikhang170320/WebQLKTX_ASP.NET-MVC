using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKyTucXa.Models
{
    public class ViewModel_HD
    {
        // Not used
        public string MAPHONG { get; set; }
        public string MADAYPHONG { get; set; }

        // In used
        public int? THANG { get; set; }

        public double DONGIA_DIEN { get; set; }
        public double DONGIA_NUOC { get; set; }
        public int? NAM { get; set; }
        public int ID_PHONG { get; set; }
        public int? NUOC_CHISODAU { get; set; }
        public int? NUOC_CHISOCUOI { get; set; }
        public int? DIEN_CHISODAU { get; set; }
        public int? DIEN_CHISOCUOI { get; set; }

        public int? CHISODIEN { get; set; }
        public int? CHISONUOC { get; set; }
        public double? THANHTIEN { get; set; }
        public double? THANHTIEN_DIEN { get; set; }
        public double? THANHTIEN_NUOC { get; set; }
        public int? KY { get; set; }
        public double? DONGIA { get;  set; }
        public int? TRANGTHAI { get;  set; }
    }
}