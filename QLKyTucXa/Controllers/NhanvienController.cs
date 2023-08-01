using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
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
                                      //nv.NGAYSINH : do kiểu datetime nên để như này
                                      NGAYSINH = nv.NGAYSINH.Day + "/" + nv.NGAYSINH.Month + "/" + nv.NGAYSINH.Year, 
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
    }
}