using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class NhanvienController : BaseController
    {
        // GET: CanBo/Nhanvien
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AllNhanvien()
        {
            try
            {
                var dsNhanvien = (from nv in db.NHANVIENs.Where(x => x.DAXOA != true)
                                  select new
                                  {
                                      ID_NHANVIEN = nv.ID_NHANVIEN,
                                      MANV = nv.MANV,
                                      TENNV = nv.TENNV,
                                      //NGAYSINH = nv.NGAYSINH,
                                      //GIOITINH = nv.GIOITINH,
                                      CMND_CCCD = nv.CMND_CCCD,
                                      SDT = nv.SDT,
                                      EMAIL = nv.EMAIL,
                                      DIACHI = nv.DIACHI,

                                  }).ToList();

                return Json(new { code = 200, dsNhanvien = dsNhanvien }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách nhân viên thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult DsNhanvien(string tuKhoa, int trang)
        {
            try
            {
                var dsNhanvien = (from nv in db.NHANVIENs //lay nhung nv chua bi xoa
                            .Where(x => x.DAXOA != true && (
                                x.MANV.ToLower().Contains(tuKhoa)//hoac la ma nv co chua tu khoa
                                || x.TENNV.ToLower().Contains(tuKhoa) //ho ten co chua tu khoa
                                || x.SDT.ToLower().Contains(tuKhoa) //hoac la sodienthoai co chua tu khoa
                                || x.EMAIL.ToLower().Contains(tuKhoa)//email co chua tu khoa
                                || x.DIACHI.ToLower().Contains(tuKhoa)//hoac dia chi co chua tu khoa
                                )
                            )
                                  select new
                                  {
                                      ID_NHANVIEN = nv.ID_NHANVIEN,
                                      MANV = nv.MANV,
                                      TENNV = nv.TENNV,
                                      NGAYSINH = nv.NGAYSINH.Day + "/" + nv.NGAYSINH.Month + "/" + nv.NGAYSINH.Year, //nv.NGAYSINH
                                      GIOITINH = nv.GIOITINH,
                                      CMND_CCCD = nv.CMND_CCCD,
                                      SDT = nv.SDT,
                                      EMAIL = nv.EMAIL,
                                      DIACHI = nv.DIACHI
                                  }).ToList();


                var pageSize = 10;

                var soTrang = dsNhanvien.Count() % pageSize == 0 ? dsNhanvien.Count() / pageSize : dsNhanvien.Count() / pageSize + 1;

                var dsnv = dsNhanvien
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
                return Json(new { code = 200, soTrang = soTrang, dsNhanvien = dsnv, msg = "Lấy danh sách nhân viên thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách nhân viên thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var nv = db.NHANVIENs.SingleOrDefault(x => x.ID_NHANVIEN == id);

                return Json(new { code = 200, nv = nv }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của nhân viên thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(string maNV, string tenNV, string cmndCccd, bool gioiTinh, DateTime ngaySinh, string sdt, string email, string diaChi)
        {
            try
            {
                var chk = db.NHANVIENs.Where(x => x.MANV == maNV).Count() == 0;
                if (!chk)
                {
                    return Json(new { code = 300, msg = "Mã nhân viên này đã tồn tại trong hệ thống" }, JsonRequestBehavior.AllowGet);
                }
                var nv = new NHANVIEN();

                nv.MANV = maNV;
                nv.TENNV = tenNV;
                nv.GIOITINH = gioiTinh;
                nv.NGAYSINH = ngaySinh;
                nv.CMND_CCCD = cmndCccd;
                nv.SDT = sdt;
                nv.EMAIL = email;
                nv.DIACHI = diaChi;
                //nv.NGAYSINH = DateTime.Parse(ngaySinh);

                db.NHANVIENs.Add(nv);

                db.SaveChanges();

                return Json(new { code = 200, msg = "Thêm mới nhân viên thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới nhân viên thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int id, string maNV, string tenNV, string cmndCccd, bool gioiTinh, DateTime ngaySinh, string sdt, string email, string diaChi)
        {
            try
            {
                var nv = db.NHANVIENs.SingleOrDefault(x => x.ID_NHANVIEN == id);

                nv.MANV = maNV;
                nv.TENNV = tenNV;
                nv.GIOITINH = gioiTinh;
                nv.NGAYSINH = ngaySinh;
                nv.CMND_CCCD = cmndCccd;
                nv.SDT = sdt;
                nv.EMAIL = email;
                nv.DIACHI = diaChi;
                //nv.NGAYSINH = DateTime.Parse(ngaySinh);

                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật thông tin nhân viên thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật thông tin nhân viên thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Xoa(int id)
        {
            try
            {
                var nv = db.NHANVIENs.SingleOrDefault(x => x.ID_NHANVIEN == id);
                nv.DAXOA = true;
                db.SaveChanges();

                return Json(new { code = 200, msg = "Xóa nhân viên thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa nhân viên thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}