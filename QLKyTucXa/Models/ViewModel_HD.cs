using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKyTucXa.Models
{
    public class ViewModel_HD
    {
        public int ID_PHONG { get; set; }
        public int ID_HOADONDIENNUOC { get; set; }
        public int ID_HOADONPHONG { get; set; }
        public int ID_DIEN { get; set; }
        public int ID_NUOC { get; set; }
        public int ID_DIENNUOC { get; set; }
        public int ID_DONGIA { get; set; }
        // Not used
        public string MAPHONG { get; set; }
        public string MADAYPHONG { get; set; }

        // In used
        public int THANGDIEN { get; set; }
        public int THANG { get; set; }
        public int THANGNUOC { get; set; }
        public int NAM { get; set; }
        public int NAMDIEN { get; set; }
        public int NAMNUOC { get; set; }

        public double DONGIA_DIEN { get; set; }
        public double DONGIA_NUOC { get; set; }

        public int? NUOCTHANGDAU { get; set; }
        public int? NUOCTHANGSAU { get; set; }
        public int? DIENTHANGDAU { get; set; }
        public int? DIENTHANGSAU { get; set; }

        public int? CHISODIEN { get; set; }
        public int? CHISONUOC { get; set; }
        public double? THANHTIEN { get; set; }
        public double? TIENDIEN { get; set; }
        public double? TIENNUOC { get; set; }
        public int KY { get; set; }
        public double? DONGIA { get; set; }
        public double? DONGIADIEN { get; set; }
        public double? DONGIANUOC { get; set; }
        public int? TRANGTHAIHDP { get; set; }
        public int TRANGTHAIHDDN { get; set; }
        public int TRANGTHAIDIEN { get; set; }
        public int TRANGTHAINUOC { get; set; }
    }
}