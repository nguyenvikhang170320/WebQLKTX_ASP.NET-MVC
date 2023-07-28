using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class DIENNUOCsController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CanBo/DIENNUOCs
        // GET: CanBo/DIENNUOCs
        public ActionResult Index()
        {

            return View();
        }





        [HttpGet]
        public JsonResult DSDN1()
        {
            try
            {
                var join1 = (from phong in db.PHONGs
                             join dien in db.CONGTODIENs on phong.ID_PHONG equals dien.ID_PHONG into dien1
                             from d in dien1.DefaultIfEmpty()
                             join nuoc in db.CONGTONUOCs on new { phong.ID_PHONG } equals new { nuoc.ID_PHONG } into nuoc1
                             from n in nuoc1.DefaultIfEmpty().Where(n => n.CHISODAU != d.CHISODAU)
                             where (n.ID_PHONG == d.ID_PHONG)
                             orderby (d.THANG) descending
                             orderby (d.NAM) descending

                             select new QLKyTucXa.Areas.CanBo.Models.DienNuoc
                             {
                                 ID_DIEN = d.ID_DIEN,
                                 ID_NUOC = n.ID_NUOC,

                                 MAPHONG = phong.MAPHONG,
                                 MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                                 DIENCHISODAU = d.CHISODAU,
                                 DIENCHISOCUOI = d.CHISOCUOI,
                                 NUOCCHISODAU = n.CHISODAU,
                                 NUOCCHISOCUOI = n.CHISOCUOI,
                                 THANGDIEN = d.THANG,
                                 NAMDIEN = d.NAM,
                                 THANGNUOC = n.THANG,
                                 NAMNUOC = n.NAM,
                                 TRANGTHAIDIEN = d.TRANGTHAI,
                                 TRANGTHAINUOC = n.TRANGTHAI

                             });
                return Json(new { code = 200, join1 = join1, msg = "Load danh sách điện nước thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Load danh sách điện nước thất bại:" + e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Add(int? dienchisodau, int? dienchisocuoi, int? nuocthangnay, int? nuocthangsau, int? thangdien, int? namdien, int? thangnuoc, int? namnuoc, int? trangthaidien, int? trangthainuoc)
        {
            try
            {
                int uid = Convert.ToInt32(Session["id"]);
                var l = new CONGTODIEN();
                l.ID_PHONG = uid;
                l.CHISODAU = dienchisodau;
                l.CHISOCUOI = dienchisocuoi;
                l.THANG = thangdien;
                l.NAM = namdien;
                l.TRANGTHAI = trangthaidien;
                db.CONGTODIENs.Add(l);
                var l1 = new CONGTONUOC();
                l1.ID_PHONG = uid;
                l1.CHISODAU = nuocthangnay;
                l1.CHISOCUOI = nuocthangsau;
                l1.THANG = thangnuoc;
                l1.NAM = namnuoc;
                l1.TRANGTHAI = trangthainuoc;
                db.CONGTONUOCs.Add(l1);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm danh sách điện nước thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Thêm danh sách điện nước thất bại:" + e.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }
        // cập nhật điện
        [HttpGet]
        public JsonResult ChitietDien(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l = db.CONGTODIENs.SingleOrDefault(x => x.ID_DIEN == id);
                return Json(new { code = 200, L = l, msg = "Lấy danh sách theo id điện thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Lấy danh sách theo id điện không thành công!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // xử lý khi bấm button lưu chỉnh sửa điện
        [HttpPost]
        public JsonResult CapnhatDien(int id, int? dienchisodau, int? dienchisocuoi, int? trangthaidien)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l = db.CONGTODIENs.SingleOrDefault(x => x.ID_DIEN == id);
                l.CHISODAU = dienchisodau;
                l.CHISOCUOI = dienchisocuoi;
                l.TRANGTHAI = trangthaidien;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Cập nhật danh sách điện thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Cập nhật danh sách điện thất bại:" + e.Message
                }, JsonRequestBehavior.AllowGet);
            }



        }

        // cập nhật nước
        [HttpGet]
        public JsonResult ChitietNuoc(int id1)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l1 = db.CONGTONUOCs.SingleOrDefault(x => x.ID_NUOC == id1);
                return Json(new { code = 200, L1 = l1, msg = "Lấy danh sách theo id nước thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Lấy danh sách theo id nước không thành công!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // xử lý khi bấm button lưu chỉnh sửa nước
        [HttpPost]
        public JsonResult CapnhatNuoc(int id1, int? nuocthangnay, int? nuocthangsau, int? trangthainuoc)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l1 = db.CONGTONUOCs.SingleOrDefault(x => x.ID_NUOC == id1);
                l1.CHISODAU = nuocthangnay;
                l1.CHISOCUOI = nuocthangsau;
                l1.TRANGTHAI = trangthainuoc;
                db.SaveChanges();
                return Json(new { code = 200, msg = "Cập nhật danh sách nước thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Cập nhật danh sách nước thất bại:" + e.Message
                }, JsonRequestBehavior.AllowGet);
            }



        }

        // Delete điện
        [HttpPost]
        public JsonResult XoaDien(int id, int? dienchisodau, int? dienchisocuoi, int? trangthaidien)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l = db.CONGTODIENs.SingleOrDefault(x => x.ID_DIEN == id);
                l.CHISODAU = dienchisodau;
                l.CHISOCUOI = dienchisocuoi;
                l.TRANGTHAI = trangthaidien;
                db.CONGTODIENs.Remove(l);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa danh sách điện thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Xóa danh sách điện thất bại:" + e.Message
                }, JsonRequestBehavior.AllowGet);
            }



        }

        // Delete điện
        [HttpGet]
        public JsonResult XoaNuoc(int id1, int? nuocthangnay, int? nuocthangsau, int? trangthainuoc)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var l1 = db.CONGTONUOCs.SingleOrDefault(x => x.ID_NUOC == id1);
                l1.CHISODAU = nuocthangnay;
                l1.CHISOCUOI = nuocthangsau;
                l1.TRANGTHAI = trangthainuoc;
                db.CONGTONUOCs.Remove(l1);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa danh sách nước thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Xóa danh sách nước thất bại:" + e.Message
                }, JsonRequestBehavior.AllowGet);
            }



        }
    }
}


