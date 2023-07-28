using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class DayphongController : BaseController
    {
        // GET: CanBo/Dayphong
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DsDayphong(string tuKhoa, int trang)
        {
            try
            {

                var dsDayphong = (from dp in db.DAYPHONGs
                             .Where(x => x.DAXOA != true && x.MADAYPHONG.ToLower().Contains(tuKhoa))
                                  select new
                                  {
                                      ID_DAY = dp.ID_DAY,
                                      MADAYPHONG = dp.MADAYPHONG
                                  }).ToList();
                //30 dòng 40 dòng chẳng hạn ...

                var pageSize = 10;

                var soTrang = dsDayphong.Count() % pageSize == 0 ? dsDayphong.Count() / pageSize : dsDayphong.Count() / pageSize + 1;

                var dsdp = dsDayphong
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
                return Json(new { code = 200, soTrang = soTrang, dsDayphong = dsdp, msg = "Lấy danh sách dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách dãy phòng thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }


        [HttpGet]
        public JsonResult AllDayphong()
        {
            try
            {
                var allDayphong = (from dp in db.DAYPHONGs.Where(x => x.DAXOA != true)
                                   select new
                                   {
                                       ID_DAY = dp.ID_DAY,
                                       MADAYPHONG = dp.MADAYPHONG
                                   }).ToList();
                return Json(new { code = 200, allDayphong = allDayphong, msg = "Load danh sách dãy phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Load danh sách dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(string maDayphong)
        {
            try
            {
                var dp = new DAYPHONG();
                dp.MADAYPHONG = maDayphong;
                dp.DAXOA = false;

                db.DAYPHONGs.Add(dp);//them doi tuong day phong dc khai bao o phia tren
                db.SaveChanges();//luu vao csdl

                return Json(new { code = 200, msg = "Thêm dãy phòng mới thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới dãy phòng thất bại. Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var dp = db.DAYPHONGs.SingleOrDefault(x => x.ID_DAY == id);
                return Json(new { code = 200, dp = dp, msg = "Lấy thông tin chi tiết của dãy phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult CapNhat(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var dp = db.DAYPHONGs.SingleOrDefault(x => x.ID_DAY == id);
                return Json(new { code = 200, dp = dp, msg = "Lấy thông tin cập nhật của dãy phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int id, string maDayphong)
        {
            try
            {
                //tìm dãy phòng dựa vào id
                var dp = db.DAYPHONGs.SingleOrDefault(x => x.ID_DAY == id);

                //gán lại các thuộc tính của dãy phòng đc tìm thấy
                dp.MADAYPHONG = maDayphong;

                //lưu lại csdl
                db.SaveChanges();

                return Json(new { code = 200, dp = dp, msg = "Cập nhật dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Xoa(int id)
        {
            try
            {
                var dp = db.DAYPHONGs.SingleOrDefault(x => x.ID_DAY == id);
                dp.DAXOA = true;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa dãy phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}