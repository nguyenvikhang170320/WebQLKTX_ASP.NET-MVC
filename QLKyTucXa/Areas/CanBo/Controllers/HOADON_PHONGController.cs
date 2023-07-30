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
                  from  phong in db.PHONGs.Where(x=>x.TRANGTHAI == true && x.DAXOA != true)
                  join hoadon_phong in db.HOADON_PHONG on phong.ID_PHONG equals hoadon_phong.ID_PHONG into tableA
                  from tA in tableA.DefaultIfEmpty()
                  where(phong.MAPHONG.ToLower().Contains(tuKhoa))
                  || phong.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                  select new ViewModel_HDĐN_HDP
                  {
                      ID_PHONG = phong.ID_PHONG,
                      //ID_HOADONPHONG = tA.ID_HOADONPHONG,
                      MAPHONG = phong.MAPHONG,
                      MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                      NAM = tA.NAM,
                      KY = tA.KY,
                      DONGIA = phong.DONGIA,
                      THANHTIEN = phong.DONGIA * 6,
                      TRANGTHAI = tA.TRANGTHAI
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
                return Json(new { code = 200, dsp = dsp, msg = "Lấy danh sách dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(int idP, int? nam, int? ky, int? trangthai)
        {
            try
            {
                //var chk = db.HOADON_PHONG.Where(x => x.NAM == nam).Count() == 0;
              
                
                //if (!chk)
                //{
                //    return Json(new { code = 300, msg = "Năm này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                //}
               
                var p = new HOADON_PHONG();
                p.ID_PHONG = idP;
               
                p.NAM = nam;
                p.KY = ky;
                p.TRANGTHAI = trangthai;
               
                db.HOADON_PHONG.Add(p);
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
