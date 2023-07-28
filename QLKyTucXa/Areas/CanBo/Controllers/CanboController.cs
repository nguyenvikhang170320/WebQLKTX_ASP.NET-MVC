using Model.EF;
using QLKyTucXa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class CanboController : BaseController
    {
        // GET: CanBo/Canbo
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AllCanbo()
        {
            try
            {
                var dscb = (from cb in db.CANBOes.Where(x => x.DAXOA != true)
                            select new
                            {
                                ID_CANBO = cb.ID_CANBO,
                                MACB = cb.MACB,
                                TAIKHOAN = cb.TAIKHOAN,
                                TENCB = cb.TENCB
                            }).ToList();

                return Json(new { code = 200, dscb = dscb }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult DsCanbo(string tuKhoa, int trang)
        {
            try
            {
                int idcb = Convert.ToInt32(Session["idcb"]);
                var qt = db.CANBOes.SingleOrDefault(x => x.ID_CANBO == idcb).QUANTRI == true ? true : false;
                var dscb = (from cb in db.CANBOes
                            .Where(x => x.DAXOA != true //lay nhung cb chua bi xoa
                                && (
                                x.MACB.ToLower().Contains(tuKhoa) //ho ten co chua tu khoa
                                || x.TAIKHOAN.ToLower().Contains(tuKhoa)//hoac dia chi co chua tu khoa
                                || x.TENCB.ToLower().Contains(tuKhoa)//hoac la magv co chua tu khoa
                                || x.EMAIL.ToLower().Contains(tuKhoa)//email co chua tu khoa
                                || x.SDT.ToLower().Contains(tuKhoa) //hoac la sodienthoai co chua tu khoa
                                )
                            )
                            select new
                            {
                                ID_CANBO = cb.ID_CANBO,
                                MACB = cb.MACB,
                                TAIKHOAN = cb.TAIKHOAN,
                                MATKHAU = cb.MATKHAU,
                                TENCB = cb.TENCB,
                                GIOITINH = cb.GIOITINH,
                                CMND_CCCD = cb.CMND_CCCD,
                                DIACHI = cb.DIACHI,
                                EMAIL = cb.EMAIL,
                                SDT = cb.SDT,
                                //QUANTRI = cb.QUANTRI //true
                            }).ToList();


                var pageSize = 10;

                var soTrang = dscb.Count() % pageSize == 0 ? dscb.Count() / pageSize : dscb.Count() / pageSize + 1;

                var kqht = dscb
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
                return Json(new { code = 200, soTrang = soTrang, dscb = kqht, qt = qt, idcb = idcb, msg = "Lấy danh sách cán bộ thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var cb = db.CANBOes.SingleOrDefault(x => x.ID_CANBO == id);
                return Json(new { code = 200, cb = cb }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ThemMoi(string maCB, string taiKhoan, string matKhau, string tenCB, bool gioiTinh, string cmndCccd, string diaChi, string email, string sdt) //, bool quanTri
        {
            try
            {
                var chk = db.CANBOes.Where(x => x.TAIKHOAN == taiKhoan).Count() == 0;
                var chk1 = db.CANBOes.Where(x => x.MACB == maCB).Count() == 0;
                if (!chk)
                {
                    return Json(new { code = 300, msg = "Tài khoản cán bộ này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                }
                if (!chk1)
                {
                    return Json(new { code = 301, msg = "Mã cán bộ này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                }
                var cb = new CANBO();
                var encryptedMd5Pas = Encryptor.MD5Hash(matKhau);

                cb.MACB = maCB;
                cb.TAIKHOAN = taiKhoan;
                cb.MATKHAU = encryptedMd5Pas;
                cb.TENCB = tenCB;
                cb.GIOITINH = gioiTinh;
                cb.CMND_CCCD = cmndCccd;
                cb.DIACHI = diaChi;
                cb.EMAIL = email;
                cb.SDT = sdt;
                cb.QUANTRI = false;
                cb.DAXOA = false;

                db.CANBOes.Add(cb);

                db.SaveChanges();

                return Json(new { code = 200, msg = "Thêm mới cán bộ thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm mới cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CapNhat(int id, string diaChi, string email, string sdt) //, string maCB, string taiKhoan, string matKhau, string tenCB, bool gioiTinh, string cmndCccd, bool quanTri
        {
            try
            {
                var cb = db.CANBOes.SingleOrDefault(x => x.ID_CANBO == id);
                //var encryptedMd5Pas = Encryptor.MD5Hash(matKhau);

                //cb.MACB = maCB;
                //cb.TAIKHOAN = taiKhoan;
                //cb.MATKHAU = encryptedMd5Pas;
                //cb.TENCB = tenCB;
                //cb.GIOITINH = gioiTinh;
                //cb.CMND_CCCD = cmndCccd;
                cb.DIACHI = diaChi;
                cb.EMAIL = email;
                cb.SDT = sdt;
                //cb.QUANTRI = false;
                //cb.DAXOA = false;

                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật thông tin cán bộ thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật thông tin cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Xoa(int id)
        {
            try
            {
                var cb = db.CANBOes.SingleOrDefault(x => x.ID_CANBO == id);
                cb.DAXOA = true;
                db.SaveChanges();

                return Json(new { code = 200, msg = "Xóa cán bộ thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
        public JsonResult XacNhan(int id)
        {
            try
            {
                var cb = db.CANBOes.SingleOrDefault(x => x.ID_CANBO == id);
                cb.QUANTRI = true;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm quản trị viên thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm quản trị viên thất bại: "+ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ListApply()
        {
            try
            {
                var apply = (from cb in db.CANBOes.Where(x => x.QUANTRI != true && x.DAXOA != true)
                          select new
                          {
                              ID_CANBO = cb.ID_CANBO,
                              MACB = cb.MACB,
                              TAIKHOAN = cb.TAIKHOAN,
                              MATKHAU = cb.MATKHAU,
                              TENCB = cb.TENCB,
                              GIOITINH = cb.GIOITINH,
                              CMND_CCCD = cb.CMND_CCCD,
                              DIACHI = cb.DIACHI,
                              EMAIL = cb.EMAIL,
                              SDT = cb.SDT
                          }).ToList();
                return Json(new { code = 200, apply = apply, msg = "Lấy danh sách cán bộ quản lý thành công!" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách cán bộ quản lý thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ChiTietPass(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var cb = db.CANBOes.SingleOrDefault(x => x.ID_CANBO == id);
                return Json(new { code = 200, cb = cb }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Reset(int id, string matKhau0, string matKhau2) //, string matKhau1, string maCB, string taiKhoan, string matKhau, string tenCB, bool gioiTinh, string cmndCccd, bool quanTri
        {
            try
            {
                var cb = db.CANBOes.SingleOrDefault(x => x.ID_CANBO == id);
                var encryptedMd5Pas = Encryptor.MD5Hash(matKhau0);
                //var encryptedMd5Pas1 = Encryptor.MD5Hash(matKhau1);
                var encryptedMd5Pas2 = Encryptor.MD5Hash(matKhau2);
                var pass = cb.MATKHAU;

                if (encryptedMd5Pas == pass)
                {
                    cb.MATKHAU = encryptedMd5Pas2;
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Thay đổi mật khẩu cán bộ thành công" }, JsonRequestBehavior.AllowGet);
                } else if (encryptedMd5Pas != pass)
                {
                    return Json(new { code = 300, msg = "Bạn nhập sai mật khẩu hiện tại!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { code = 501, msg = "Bạn chưa nhập mật khẩu hiện tại!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thay đổi mật khẩu cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}