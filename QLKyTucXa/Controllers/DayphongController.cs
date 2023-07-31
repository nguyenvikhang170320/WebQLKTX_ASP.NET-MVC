using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;


namespace QLKyTucXa.Controllers
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

    }
}