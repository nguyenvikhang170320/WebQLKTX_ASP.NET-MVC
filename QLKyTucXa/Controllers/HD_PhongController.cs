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
                      from hdp in db.HOADON_PHONG.Where(x=> x.ID_PHONG == uid)
                      join phong in db.PHONGs on hdp.ID_PHONG equals phong.ID_PHONG into tableA
                      from tA in tableA.Where(x => x.TRANGTHAI == true && x.DAXOA != true && x.ID_PHONG == uid).DefaultIfEmpty()
                      where (hdp.PHONG.MAPHONG.ToLower().Contains(tuKhoa))
                      || hdp.PHONG.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                      select new ViewModel_HD()
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
      
    }

}