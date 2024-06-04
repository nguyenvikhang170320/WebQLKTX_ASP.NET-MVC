using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using QLKyTucXa.Areas.CanBo.Models;
using QLKyTucXa.Common;
using QLKyTucXa.Models;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class HOADON_PHONGController : Controller
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CanBo/HOADON_PHONG
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DsHDPhong(string tuKhoa, int trang)//, int idDp
        {
            try
            {

                var dshdp = (
                      from hdp in db.HOADON_PHONG
                      join phong in db.PHONGs on hdp.ID_PHONG equals phong.ID_PHONG into tableA
                      from tA in tableA.Where(x => x.TRANGTHAI == true && x.DAXOA != true).DefaultIfEmpty()
                      where (hdp.PHONG.MAPHONG.ToLower().Contains(tuKhoa))
                      || hdp.PHONG.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                      select new ViewModel_HDĐN_HDP
                      {
                          ID_HOADONPHONG = hdp.ID_HOADONPHONG,
                          ID_PHONG = hdp.ID_PHONG,
                          MAPHONG = hdp.PHONG.MAPHONG,
                          MADAYPHONG = hdp.PHONG.DAYPHONG.MADAYPHONG,
                          NAM = hdp.NAM,
                          KY = hdp.KY,
                          DONGIA = hdp.PHONG.DONGIA,
                          THANHTIEN = hdp.PHONG.DONGIA * 6,
                          TRANGTHAIHDP = hdp.TRANGTHAI
                      }).ToList();


                var pageSize = 10;

                var soTrang = dshdp.Count() % pageSize == 0 ? dshdp.Count() / pageSize : dshdp.Count() / pageSize + 1;

                var kqht = dshdp
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();


                return Json(new { code = 200, dshdp = kqht, soTrang = soTrang, msg = "Lấy danh sách hóa đơn phòng thành công!" }, JsonRequestBehavior.AllowGet); //, isTBM = isTBM, idDangNhap = gv.Id
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
                return Json(new { code = 200, dsp = dsp, msg = "Lấy danh sách phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult ThemMoi(int idP, int nam, int ky, int? trangthai)
        {
            try
            {
                //var chk = db.HOADON_PHONG.Where(x => x.NAM == nam).Count() == 0;
              
                
                //if (!chk)
                //{
                //    return Json(new { code = 300, msg = "Năm này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                //}
               
                var hdp = new HOADON_PHONG();
                hdp.ID_PHONG = idP;

                hdp.NAM = nam;
                hdp.KY = ky;
                hdp.TRANGTHAI = trangthai;
               
                db.HOADON_PHONG.Add(hdp);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm mới hóa đơn phòng thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới hóa đơn phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CapNhat(int id, int nam, int ky, int? trangthai) //, int idDp, string maPhong, string taiKhoan, string matKhau
        {
            try
            {
                //tìm ra phòng cần cập nhật dựa vào id truyền vào
                var hdp = db.HOADON_PHONG.SingleOrDefault(x => x.ID_HOADONPHONG == id); // lỗi khúc này
                //var encryptedMd5Pas = Encryptor.MD5Hash(matKhau);
                int uid = Convert.ToInt32(Session["idcb"]);
                if (uid == 1)
                {
                    hdp.NAM = nam;
                    hdp.KY = ky;

                    hdp.TRANGTHAI = trangthai;


                    //luu vao csdl
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Cập nhật hóa đơn phòng thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 200, msg = "Tài khoản của bạn không có quyền chỉnh sửa" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Chi tiết hóa đơn phòng
        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var hdp = db.HOADON_PHONG.SingleOrDefault(x => x.ID_HOADONPHONG == id);
                return Json(new { code = 200, hdp = hdp }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
