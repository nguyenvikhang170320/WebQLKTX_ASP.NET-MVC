using Model.EF;
using QLKyTucXa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class HotroController : BaseController
    {
        // GET: CanBo/Dongia
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult DsHT(string tuKhoa, int trang)
        {
            try
            {

                var dsHT = (from dg in db.HOTROes
                            join p in db.PHONGs on dg.ID_PHONG equals p.ID_PHONG into phong
                            from ph in phong.Where(x=>x.TRANGTHAI == true && x.DAXOA == false)
                             where(ph.MAPHONG.ToLower().Contains(tuKhoa))
                             select new 
                                {
                                    ID_PHONG = ph.ID_PHONG,
                                    MAPHONG = ph.MAPHONG,
                                    ID_PHIEU = dg.ID_PHIEU,
                                    NOIDUNG = dg.NOIDUNG,
                                    TRANGTHAI = dg.TRANGTHAI,
                                    NGAYGUI = dg.NGAYGUI.Day + "/" + dg.NGAYGUI.Month + "/" + dg.NGAYGUI.Year
                             }).ToList();

                //30 dòng 40 dòng chẳng hạn ...
                var pageSize = 10;
                var soTrang = dsHT.Count() % pageSize == 0 ? dsHT.Count() / pageSize : dsHT.Count() / pageSize + 1;

                var dsdg = dsHT
                            .Skip((trang - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();
                return Json(new { code = 200, soTrang = soTrang, dsHT = dsdg, msg = "Lấy danh sách hỗ trợ thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách hỗ trợ thất bại: " + ex.Message, JsonRequestBehavior.AllowGet });
            }
        }

       
        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                var dsp = (from p in db.PHONGs
                           join ht in db.HOTROes on p.ID_PHONG equals ht.ID_PHONG
                           where (ht.ID_PHONG == id)
                           select new
                           {
                               ID_PHIEU = ht.ID_PHIEU,
                               ID_PHONG = p.ID_PHONG,
                               MAPHONG = p.MAPHONG,
                               NOIDUNG = ht.NOIDUNG,
                               NGAYGUI = ht.NGAYGUI,
                               TRANGTHAI = ht.TRANGTHAI,
                               
                           }).ToList();
                return Json(new { code = 200, msg = "Lấy thông tin phòng thành công!", ht = dsp.Count() > 0 ? dsp[0] : null }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CapNhat(int id, string noidung, DateTime ngaygui, int trangthai)
        {
            try
            {
                //tìm ra phòng cần cập nhật dựa vào id truyền vào
                var p = db.HOTROes.SingleOrDefault(x => x.ID_PHIEU == id);
                
           
                p.NOIDUNG = noidung;
                p.NGAYGUI = ngaygui;
                p.TRANGTHAI = trangthai;
              

                //luu vao csdl
                db.SaveChanges();

                return Json(new { code = 200, msg = "Cập nhật hỗ trợ thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Cập nhật hỗ trợ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        // danh sách mã phòng
        [HttpGet]
        public JsonResult ListPhong()
        {
            try
            {
                var dsp = (from p in db.PHONGs.Where(x => x.DAXOA != true)
                           select new
                           {
                               ID_PHONG = p.ID_PHONG,
                               MAPHONG = p.MAPHONG

                           }).ToList();
                return Json(new { code = 200, dsp = dsp, msg = "Lấy danh sách phòng thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách phòng thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}