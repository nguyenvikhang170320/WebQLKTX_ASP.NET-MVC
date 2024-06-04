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
                var dshddn = (from hddn in db.HOADON_DIENNUOC
                               join p in db.PHONGs on hddn.ID_PHONG equals p.ID_PHONG into tableA
                               from tA in tableA.Where(x => x.TRANGTHAI == true && x.DAXOA == false).DefaultIfEmpty()
                               join dien in db.CONGTODIENs on hddn.ID_PHONG equals dien.ID_PHONG into tableB
                               from tB in tableB.Where(x => x.THANG == hddn.THANG && x.NAM == hddn.NAM).DefaultIfEmpty()
                               join nuoc in db.CONGTONUOCs on hddn.ID_PHONG equals nuoc.ID_PHONG into tableC
                               from tC in tableC.Where(x => x.THANG == hddn.THANG && x.NAM == hddn.NAM).DefaultIfEmpty()
                               join dongia in db.DONGIAs on hddn.ID_DONGIA equals dongia.ID_DONGIA into tableD
                               from tD in tableD.Where(x=>x.TRANGTHAI == true).DefaultIfEmpty()
                               where (hddn.PHONG.MAPHONG.ToLower().Contains(tuKhoa))
                                     || hddn.PHONG.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                               select new ViewModel_HDĐN_HDP()
                               {
                                   ID_HOADONDIENNUOC = hddn.ID_HOADONDIENNUOC,
                                   ID_PHONG = hddn.ID_PHONG,
                                   ID_DONGIA = tD.ID_DONGIA,
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
                                   TRANGTHAIHDDN = hddn.TRANGTHAI

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
                                MADONGIA = ds.MADONGIA,
                                TRANGTHAI = true,
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

        [HttpPost]
        public JsonResult ThemMoi(int idP, int idDg,  int thang, int nam, int trangthai)
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
                p.ID_DONGIA = idDg; // suy nghĩ chỗ này sau lấy được đơn giá

                p.NAM = nam;
                p.THANG = thang;
                p.TRANGTHAI = trangthai;

                db.HOADON_DIENNUOC.Add(p);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm mới hóa đơn điện nước thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới hóa đơn điện nước thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        //chỉnh sửa hóa đơn điện nước
        [HttpPost]
        public JsonResult CapNhat(int id, int idP, int idDg, int thang, int nam, int trangthai) //, int idDp, string maPhong, string taiKhoan, string matKhau
        {
            try
            {
                //tìm ra phòng cần cập nhật dựa vào id truyền vào
                var hddn = db.HOADON_DIENNUOC.SingleOrDefault(x => x.ID_HOADONDIENNUOC == id); // lỗi khúc này
                                                                                               //var encryptedMd5Pas = Encryptor.MD5Hash(matKhau);
                hddn.NAM = nam;
                hddn.THANG = thang;
                hddn.ID_PHONG = idP;
                hddn.ID_DONGIA = idDg;
                hddn.ID_CANBO = 1;
                hddn.TRANGTHAI = trangthai;


                //luu vao csdl
                db.SaveChanges();
                return Json(new { code = 200, msg = "Cập nhật hóa đơn điện nước thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
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
                return Json(new { code = 200, HDDN = hddn }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của hóa đơn điện nước thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
