using Model.EF;
using QLKyTucXa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class PhongController : BaseController
    {
        // GET: CanBo/Phong
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ThemMoi(int idDp, string maPhong, string taiKhoan, string matKhau, int soLuongnv, double donGia, int tinhTrang, bool trangThai)
        {
            try
            {
                var chk = db.PHONGs.Where(x => x.TAIKHOAN == taiKhoan).Count() == 0;
                var chk1 = db.PHONGs.Where(x => x.MAPHONG == maPhong).Count() == 0;
                if (!chk)
                {
                    return Json(new { code = 300, msg = "Tài khoản phòng này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                }
                if (!chk1)
                {
                    return Json(new { code = 301, msg = "Mã phòng này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                }
                var p = new PHONG();
                var encryptedMd5Pas = Encryptor.MD5Hash(matKhau);

                p.ID_DAY = idDp;
                p.MAPHONG = maPhong;
                p.TAIKHOAN = taiKhoan;
                p.MATKHAU = encryptedMd5Pas;
                p.SOLUONGNV = soLuongnv;
                p.DONGIA = donGia;
                p.TINHTRANG = tinhTrang;
                p.TRANGTHAI = trangThai;
                p.DAXOA = false;

                db.PHONGs.Add(p);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm mới phòng thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int id, int soLuongnv, double donGia, int tinhTrang, bool trangThai) //, int idDp, string maPhong, string taiKhoan, string matKhau
        {
            try
            {
                //tìm ra phòng cần cập nhật dựa vào id truyền vào
                var p = db.PHONGs.SingleOrDefault(x => x.ID_PHONG == id);
                //var encryptedMd5Pas = Encryptor.MD5Hash(matKhau);

                //p.ID_DAY = idDp;
                //p.MAPHONG = maPhong;
                //p.TAIKHOAN = taiKhoan;
                //p.MATKHAU = encryptedMd5Pas;
                p.SOLUONGNV = soLuongnv;
                p.DONGIA = donGia;
                p.TINHTRANG = tinhTrang;
                p.TRANGTHAI = trangThai;

                //luu vao csdl
                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật phòng thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Xoa(int id)
        {
            try
            {
                var p = db.PHONGs.SingleOrDefault(x => x.ID_PHONG == id);
                p.DAXOA = true;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Xóa phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult DsPhong(string tuKhoa, int trang)//, int idDp
        {
            try
            {
                var dsp = (from p in db.PHONGs.Where(
                                        x => x.DAXOA != true &&
                                        (x.MAPHONG.ToLower().Contains(tuKhoa)
                                            || x.TAIKHOAN.ToLower().Contains(tuKhoa)
                                        ))

                           join dp in db.DAYPHONGs on p.ID_DAY equals dp.ID_DAY

                           select new
                                {
                                    ID_PHONG = p.ID_PHONG,
                                    MADAYPHONG = dp.MADAYPHONG,
                                    MAPHONG = p.MAPHONG,
                                    TAIKHOAN = p.TAIKHOAN,
                                    MATKHAU = p.MATKHAU,
                                    SOLUONGNV = p.SOLUONGNV,
                                    DONGIA = p.DONGIA,
                                    TINHTRANG = p.TINHTRANG,
                                    TRANGTHAI = p.TRANGTHAI
                                }
                            ).ToList();


                var pageSize = 10;

                var soTrang = dsp.Count() % pageSize == 0 ? dsp.Count() / pageSize : dsp.Count() / pageSize + 1;

                var kqht = dsp
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();


                return Json(new { code = 200, dsp = kqht, soTrang = soTrang, msg = "Lấy danh sách phòng thành công!" }, JsonRequestBehavior.AllowGet); //, isTBM = isTBM, idDangNhap = gv.Id
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }


        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                var dsp = (from p in db.PHONGs
                          join dp in db.DAYPHONGs on p.ID_DAY equals dp.ID_DAY
                          where (p.ID_PHONG == id)
                          select new
                          {
                              ID_PHONG = p.ID_PHONG,
                              MADAYPHONG = dp.MADAYPHONG,
                              MAPHONG = p.MAPHONG,
                              TAIKHOAN = p.TAIKHOAN,
                              MATKHAU = p.MATKHAU,
                              SOLUONGNV = p.SOLUONGNV,
                              DONGIA = p.DONGIA,
                              TINHTRANG = p.TINHTRANG,
                              TRANGTHAI = p.TRANGTHAI,
                              ID_DAY = dp.ID_DAY
                          }).ToList();
                return Json(new { code = 200, msg = "Lấy thông tin phòng thành công!", p = dsp.Count() > 0 ? dsp[0] : null }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ListDayphong()
        {
            try
            {
                var dsdp = (from dp in db.DAYPHONGs.Where(x => x.DAXOA != true)
                          select new
                          {
                              ID_DAY = dp.ID_DAY,
                              MADAYPHONG = dp.MADAYPHONG
                          }).ToList();
                return Json(new { code = 200, dsdp = dsdp, msg = "Lấy danh sách dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ChiTietPass(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var p = db.PHONGs.SingleOrDefault(x => x.ID_PHONG == id);
                return Json(new { code = 200, p = p }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Reset(int id, string matKhau2) //, string matKhau0, string matKhau1, string maCB, string taiKhoan, string matKhau, string tenCB, bool gioiTinh, string cmndCccd, bool quanTri
        {
            try
            {
                var p = db.PHONGs.SingleOrDefault(x => x.ID_PHONG == id);
                //var encryptedMd5Pas = Encryptor.MD5Hash(matKhau0);
                //var encryptedMd5Pas1 = Encryptor.MD5Hash(matKhau1);
                var encryptedMd5Pas2 = Encryptor.MD5Hash(matKhau2);
                //var pass = cb.MATKHAU;

                p.MATKHAU = encryptedMd5Pas2;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thay đổi mật khẩu phòng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thay đổi mật khẩu phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}