using Model.EF;
using QLKyTucXa.Areas.CanBo.Models;
using QLKyTucXa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
{
    public class HD_PhongController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
        // GET: HD_Phong




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
                int uid = Convert.ToInt32(Session["idphong"]);
                var dshdp = (
                      from phong in db.PHONGs.Where(x => x.TRANGTHAI == true && x.DAXOA != true && x.ID_PHONG == uid)
                      join hoadon_phong in db.HOADON_PHONG on phong.ID_PHONG equals hoadon_phong.ID_PHONG into tableA
                      from tA in tableA.DefaultIfEmpty()
                      where (phong.MAPHONG.ToLower().Contains(tuKhoa))
                      || phong.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                      select new ViewModel_HDĐN_HDP
                      {
                          ID_PHONG = phong.ID_PHONG,
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

            ////dãy phòng
            //[HttpGet]
            //public JsonResult ListDayphong()
            //{
            //    try
            //    {
            //        var dsdp = (from dp in db.DAYPHONGs.Where(x => x.DAXOA != true)
            //                    select new
            //                    {
            //                        ID_DAY = dp.ID_DAY,
            //                        MADAYPHONG = dp.MADAYPHONG
            //                    }).ToList();
            //        return Json(new { code = 200, dsdp = dsdp, msg = "Lấy danh sách dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);

            //    }
            //    catch (Exception ex)
            //    {
            //        return Json(new { code = 500, msg = "Lấy danh sách dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            //    }
            //}

            //// phòng
            //[HttpGet]
            //public JsonResult ListPhong()
            //{
            //    try
            //    {
            //        var dsp = (from p in db.PHONGs.Where(x => x.DAXOA != true)
            //                   select new
            //                   {
            //                       ID_PHONG = p.ID_PHONG,
            //                       MAPHONG = p.MAPHONG

            //                   }).ToList();
            //        return Json(new { code = 200, dsp = dsp, msg = "Lấy danh sách dãy phòng thành công!" }, JsonRequestBehavior.AllowGet);

            //    }
            //    catch (Exception ex)
            //    {
            //        return Json(new { code = 500, msg = "Lấy danh sách dãy phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            //    }
            //}

           

    }
}