using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
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
                db.Configuration.ProxyCreationEnabled = false;

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

    }
}