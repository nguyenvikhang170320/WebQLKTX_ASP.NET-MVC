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

namespace QLKyTucXa.Controllers
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
                int uid = Convert.ToInt32(Session["idphong"]);
                var dshddn = (from hddn in db.HOADON_DIENNUOC.Where(x=>x.ID_PHONG == uid)
                               join p in db.PHONGs on hddn.ID_PHONG equals p.ID_PHONG into tableA
                               from tA in tableA.Where(x => x.TRANGTHAI == true && x.DAXOA == false).DefaultIfEmpty()
                               join dien in db.CONGTODIENs on hddn.ID_PHONG equals dien.ID_PHONG into tableB
                               from tB in tableB.Where(x => x.THANG == hddn.THANG && x.NAM == hddn.NAM).DefaultIfEmpty()
                               join nuoc in db.CONGTONUOCs on hddn.ID_PHONG equals nuoc.ID_PHONG into tableC
                               from tC in tableC.Where(x => x.THANG == hddn.THANG && x.NAM == hddn.NAM).DefaultIfEmpty()
                               join dongia in db.DONGIAs on hddn.ID_DONGIA equals dongia.ID_DONGIA into tableD
                               from tD in tableD.DefaultIfEmpty()
                               where (hddn.PHONG.MAPHONG.ToLower().Contains(tuKhoa))
                                     || hddn.PHONG.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                               select new ViewModel_HD()
                               {
                                   ID_HOADONDIENNUOC = hddn.ID_HOADONDIENNUOC,
                                   MAPHONG = hddn.PHONG.MAPHONG,
                                   MADAYPHONG = hddn.PHONG.DAYPHONG.MADAYPHONG,
                                   DIENTHANGDAU = tB.CHISODAU,
                                   DIENTHANGSAU = tB.CHISOCUOI,
                                   CHISODIEN = tB.CHISOCUOI - tB.CHISODAU,
                                   NUOCTHANGDAU = tC.CHISODAU,
                                   NUOCTHANGSAU = tC.CHISOCUOI,
                                   CHISONUOC = tC.CHISOCUOI - tC.CHISODAU,
                                   TIENDIEN = (tB.CHISOCUOI - tB.CHISODAU) * tD.DONGIADIEN,
                                   TIENNUOC = (tC.CHISOCUOI - tC.CHISODAU) * tD.DONGIANUOC,
                                   THANHTIEN = (tB.CHISOCUOI - tB.CHISODAU) * tD.DONGIADIEN + (tC.CHISOCUOI - tC.CHISODAU) * tD.DONGIANUOC,
                                   THANG = hddn.THANG,
                                   NAM = hddn.NAM,
                                   TRANGTHAIHDDN= hddn.TRANGTHAI

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
     
        
        //danh sách mã đơn giá
        [HttpGet]
        public JsonResult ListDongia()
        {
            try
            {
                var dsdg = (from ds in db.DONGIAs.Where(x => x.DAXOA != true)
                            select new
                            {
                                ID_DONGIA = ds.ID_DONGIA,
                                MADONGIA = ds.MADONGIA
                            }).ToList();
                return Json(new { code = 200, dsdg = dsdg, msg = "Lấy danh sách đơn giá thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        // danh sách mã phòng
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
                return Json(new { code = 200, dsp = dsp, msg = "Lấy danh sách phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        //Chi tiết hóa đơn điện nước
        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var hddn = db.HOADON_DIENNUOC.SingleOrDefault(x => x.ID_HOADONDIENNUOC == id);
                return Json(new { code = 200, hddn = hddn }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
