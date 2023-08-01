using Model.EF;
using QLKyTucXa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
{
    public class PhongController : BaseController
    {
        // GET: CanBo/Phong
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
        public ActionResult Index()
        {
            return View();
        }

        // load phòng
        [HttpGet]
        public JsonResult DsPhong(string tuKhoa, int trang)//, int idDp
        {
            try
            {
                int uid = Convert.ToInt32(Session["idphong"]);
                var dsp = (from p in db.PHONGs.Where(
                                        x => x.DAXOA != true && x.ID_PHONG == uid &&
                                        (x.MAPHONG.ToLower().Contains(tuKhoa)
                                            || x.TAIKHOAN.ToLower().Contains(tuKhoa)
                                        ))

                           join dp in db.DAYPHONGs on p.ID_DAY equals dp.ID_DAY

                           select new
                                {
                                    ID_PHONG = p.ID_PHONG,
                                    ID_DAYPHONG = p.ID_DAY,
                                    MADAYPHONG = dp.MADAYPHONG,
                                    MAPHONG = p.MAPHONG,
                                    TAIKHOAN = p.TAIKHOAN,
                                    MATKHAU = p.MATKHAU,
                                    SOLUONGNV = p.SOLUONGNV,
                                    DONGIA = p.DONGIA,
                                    MOTAKHAC = p.MOTAKHAC,
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


                return Json(new { code = 200, dsp = kqht, soTrang = soTrang, msg = "Lấy danh sách phòng thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
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
                int uid = Convert.ToInt32(Session["idphong"]);
                var p = db.PHONGs.SingleOrDefault(x => x.ID_PHONG == id);
                //var encryptedMd5Pas = Encryptor.MD5Hash(matKhau0);
                //var encryptedMd5Pas1 = Encryptor.MD5Hash(matKhau1);
                if (uid==id)
                {
                    var encryptedMd5Pas2 = Encryptor.MD5Hash(matKhau2);
                    //var pass = cb.MATKHAU;

                    p.MATKHAU = encryptedMd5Pas2;
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Thay đổi mật khẩu phòng thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 200, msg = "Bạn chỉ có quyền thay đổi mật khẩu phòng mình" }, JsonRequestBehavior.AllowGet);
                }
               
               
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thay đổi mật khẩu phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}