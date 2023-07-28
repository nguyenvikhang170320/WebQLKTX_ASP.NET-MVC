using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class DongiaController : BaseController
    {
        // GET: CanBo/Dongia
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult DsDongia(string tuKhoa, int trang)
        {
            try
            {

                var dsDongia = (from dg in db.DONGIAs
                             .Where(x => x.DAXOA != true && x.MADONGIA.ToLower().Contains(tuKhoa))
                                select new
                                {
                                    ID_DONGIA = dg.ID_DONGIA,
                                    MADONGIA = dg.MADONGIA,
                                    DONGIADIEN = dg.DONGIADIEN,
                                    DONGIANUOC = dg.DONGIANUOC,
                                    TRANGTHAI = dg.TRANGTHAI,
                                    NGAYAPDUNG = dg.NGAYAPDUNG.Day + "/" + dg.NGAYAPDUNG.Month + "/" + dg.NGAYAPDUNG.Year
                                }).ToList();

                //30 dòng 40 dòng chẳng hạn ...
                var pageSize = 10;
                var soTrang = dsDongia.Count() % pageSize == 0 ? dsDongia.Count() / pageSize : dsDongia.Count() / pageSize + 1;

                var dsdg = dsDongia
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
                return Json(new { code = 200, soTrang = soTrang, dsDongia = dsdg, msg = "Lấy danh sách đơn giá thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách đơn giá thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(string maDongia, double donGiadien, double donGianuoc, bool trangThai, DateTime ngayApdung)
        {
            try
            {
                var dg = new DONGIA();
                dg.MADONGIA = maDongia;
                dg.DONGIADIEN = donGiadien;
                dg.DONGIANUOC = donGianuoc;
                dg.TRANGTHAI = trangThai;
                dg.NGAYAPDUNG = ngayApdung;
                dg.DAXOA = false;

                db.DONGIAs.Add(dg);//them doi tuong don gia dc khai bao o phia tren
                db.SaveChanges();//luu vao csdl

                return Json(new { code = 200, msg = "Thêm đơn giá mới thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới đơn giá thất bại. Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult CapNhat(int id)
        {
            try
            {
                var dg = db.DONGIAs.SingleOrDefault(x => x.ID_DONGIA == id);
                return Json(new { code = 200, DG = dg, msg = "Lấy thông tin cập nhật của đơn giá thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin đơn giá thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int id, bool trangThai)
        {
            try
            {
                //tìm đơn giá dựa vào id
                var dg = db.DONGIAs.SingleOrDefault(x => x.ID_DONGIA == id);

                //gán lại các thuộc tính của đơn giá đc tìm thấy
                dg.TRANGTHAI = trangThai;

                //lưu lại csdl
                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật đơn giá thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật đơn giá thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}