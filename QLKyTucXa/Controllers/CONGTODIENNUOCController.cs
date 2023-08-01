using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using QLKyTucXa.Areas.CanBo.Models;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class CONGTODIENNUOCController : Controller
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CanBo/CONGTODIENNUOC
        public ActionResult Index()
        {
            return View();
        }


        //Chi tiết hóa đơn điện nước
        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;//cấu hình proxy cho database
                var hdp = db.HOADON_PHONG.SingleOrDefault(x => x.ID_HOADONPHONG == id);
                return Json(new { code = 200, hdp = hdp }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chi tiết của cán bộ thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public JsonResult ThemMoi(int idP, int? dienthangdau, int? dienthangsau, int? nuocthangdau, int? nuocthangsau, int thang, int nam,int thangnuoc, int namnuoc, int trangthaidien, int trangthainuoc)
        {
            try
            {
                //var chk = db.HOADON_PHONG.Where(x => x.NAM == nam).Count() == 0;


                //if (!chk)
                //{
                //    return Json(new { code = 300, msg = "Năm này đã tồn tại trong hệ thống!" }, JsonRequestBehavior.AllowGet);
                //}

                int uid = Convert.ToInt32(Session["idphong"]);
                if (uid == idP)
                {
                    var d = new CONGTODIEN();
                    d.ID_PHONG = idP;
                    d.CHISODAU = dienthangdau;
                    d.CHISOCUOI = dienthangsau;
                    d.TRANGTHAI = trangthaidien;
                    d.THANG = thang;
                    d.NAM = nam;
                    db.CONGTODIENs.Add(d);

                    var n = new CONGTONUOC();
                    n.ID_PHONG = idP;
                    n.CHISODAU = nuocthangdau;
                    n.CHISOCUOI = nuocthangsau;
                    n.TRANGTHAI = trangthainuoc;
                    n.THANG = thangnuoc;
                    n.NAM = namnuoc;
                    db.CONGTONUOCs.Add(n);

                    var p = new ViewModel_HDĐN_HDP();
                    p.ID_PHONG = d.ID_PHONG = n.ID_PHONG;
                    p.DIENTHANGDAU = d.CHISODAU;
                    p.DIENTHANGSAU = d.CHISOCUOI;
                    p.NUOCTHANGDAU = n.CHISODAU;
                    p.NUOCTHANGSAU = n.CHISOCUOI;
                    p.TRANGTHAIDIEN = d.TRANGTHAI;
                    p.TRANGTHAINUOC = n.TRANGTHAI;
                    p.THANGDIEN = d.THANG;
                    p.NAMDIEN = d.NAM;
                    p.THANGNUOC = n.THANG;
                    p.NAMNUOC = n.NAM;
                }


                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm chỉ số  điện nước thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm chỉ số điện nước thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
