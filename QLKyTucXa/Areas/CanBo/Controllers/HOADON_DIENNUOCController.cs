using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using QLKyTucXa.Areas.CanBo.Models;
using QLKyTucXa.Models;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class HOADON_DIENNUOCController : Controller
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CanBo/HOADON_DIENNUOC
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public JsonResult DsHDDN(string tuKhoa, int trang)//, int idDp
        {
            try
            {
                var dshddn = (from phong in db.PHONGs.Where(x=>x.TRANGTHAI == true && x.DAXOA == false)
                               join hoadon in db.HOADON_DIENNUOC on phong.ID_PHONG equals hoadon.ID_PHONG into tableA
                               from tA in tableA.DefaultIfEmpty()
                               join dien in db.CONGTODIENs on phong.ID_PHONG equals dien.ID_PHONG into tableB
                               from tB in tableB.Where(x => x.THANG == tA.THANG && x.NAM == tA.NAM).DefaultIfEmpty()
                               join nuoc in db.CONGTONUOCs on phong.ID_PHONG equals nuoc.ID_PHONG into tableC
                               from tC in tableC.Where(x => x.THANG == tB.THANG && x.NAM == tB.NAM).DefaultIfEmpty()
                               join dongia in db.DONGIAs on tA.ID_DONGIA equals dongia.ID_DONGIA into tableD
                               from tD in tableD.DefaultIfEmpty()
                               where (phong.MAPHONG.ToLower().Contains(tuKhoa))
                                     || phong.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                               select new ViewModel_HDĐN_HDP
                               {
                                   MAPHONG = phong.MAPHONG,
                                   MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                                   DIENCHISODAU = tB.CHISODAU,
                                   DIENCHISOCUOI = tB.CHISOCUOI,
                                   CHISODIEN = tB.CHISOCUOI - tB.CHISODAU,
                                   NUOCCHISODAU = tC.CHISODAU,
                                   NUOCCHISOCUOI = tC.CHISOCUOI,
                                   CHISONUOC = tC.CHISOCUOI - tC.CHISODAU,
                                   THANHTIEN_DIEN = (tB.CHISOCUOI - tB.CHISODAU) * tD.DONGIADIEN,
                                   THANHTIEN_NUOC = (tC.CHISOCUOI - tC.CHISODAU) * tD.DONGIANUOC,
                                   THANHTIEN = (tB.CHISOCUOI - tB.CHISODAU) * tD.DONGIADIEN + (tC.CHISOCUOI - tC.CHISODAU) * tD.DONGIANUOC,
                                   THANG = tA.THANG,
                                   NAM = tA.NAM

                               }).ToList().Distinct();


                var pageSize = 10;

                var soTrang = dshddn.Count() % pageSize == 0 ? dshddn.Count() / pageSize : dshddn.Count() / pageSize + 1;

                var kqht = dshddn
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();


                return Json(new { code = 200, dshddn = kqht, soTrang = soTrang, msg = "Lấy danh sách hóa đơn phòng thành công!" }, JsonRequestBehavior.AllowGet); //, isTBM = isTBM, idDangNhap = gv.Id
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }
     
        

        // phòng
        [HttpGet]
        public JsonResult ListPhong()
        {
            try
            {
                var dsp = (from p in db.PHONGs.Where(x => x.DAXOA != true)
                           select new
                           {
                               ID_PHONG = p.ID_PHONG,
                               MAPHONG = p.MAPHONG

                           }).ToList();
                return Json(new { code = 200, dsp = dsp, msg = "Lấy danh sách dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(int idP,  int thang, int nam, int? trangthai)
        {
            try
            {
                //var chk = db.HOADON_PHONG.Where(x => x.NAM == nam).Count() == 0;


                //if (!chk)
                //{
                //    return Json(new { code = 300, msg = "Năm này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                //}

                int uid = Convert.ToInt32(Session["idcb"]);
                var p = new HOADON_DIENNUOC();
                p.ID_PHONG = idP;
                p.ID_CANBO = uid;
                p.ID_DONGIA = 1; // suy nghĩ chỗ này sau lấy được đơn giá

                p.NAM = nam;
                p.THANG = thang;
                p.TRANGTHAI = trangthai;

                db.HOADON_DIENNUOC.Add(p);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm mới hóa đơn phòng thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới hóa đơn phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
