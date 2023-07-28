using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace QLKyTucXa.Controllers
{
    public class CONGTODIENNUOCsController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CONGTODIENNUOCs
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult DSDN2()
        {
            int uid = Convert.ToInt32(Session["idphong"]);
            try
            {
                var join2 = (from phong in db.PHONGs
                             join dien in db.CONGTODIENs on phong.ID_PHONG equals dien.ID_PHONG into dien1
                             from d in dien1.DefaultIfEmpty()
                             join nuoc in db.CONGTONUOCs on new { d.ID_PHONG } equals new { nuoc.ID_PHONG } into nuoc1
                             from n in nuoc1.DefaultIfEmpty()
                             where (phong.ID_PHONG == uid && phong.TRANGTHAI == true)
                             orderby (d.THANG) descending
                             orderby (d.NAM) descending

                             select new Areas.CanBo.Models.DienNuoc
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
                return Json(new { code = 200, join2 = join2, msg = "Load danh sách điện nước thành công!" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult Add2(int? dienchisodau, int? dienchisocuoi, int? nuocthangnay, int? nuocthangsau, int? thangdien, int? namdien, int? thangnuoc, int? namnuoc, int? trangthaidien, int? trangthainuoc)
        {
            try
            {
                int uid = Convert.ToInt32(Session["idphong"]);
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
                var l2 = new Areas.CanBo.Models.DienNuoc();
                l2.DIENCHISODAU = l.CHISODAU;
                l2.DIENCHISOCUOI = l.CHISOCUOI;
                l2.NUOCCHISODAU = l1.CHISODAU;
                l2.NUOCCHISOCUOI = l1.CHISOCUOI;
                l2.THANGDIEN = l.THANG;
                l2.THANGNUOC = l1.THANG;
                l2.NAMDIEN = l.NAM;
                l2.NAMNUOC = l1.NAM;
                l2.TRANGTHAIDIEN = l.TRANGTHAI;
                l2.TRANGTHAINUOC = l1.TRANGTHAI;
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
        public JsonResult ChitietDien2(int id)
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
        public JsonResult CapnhatDien2(int id, int? dienchisodau, int? dienchisocuoi, int? trangthaidien)
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
        public JsonResult ChitietNuoc2(int id1)
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
        public JsonResult CapnhatNuoc2(int id1, int? nuocthangnay, int? nuocthangsau, int? trangthainuoc)
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
        //[HttpPost]
        // public JsonResult XoaDien(int id, int? dienchisodau, int? dienchisocuoi, string trangthaidien)
        // {
        //     try
        //     {
        //         db.Configuration.ProxyCreationEnabled = false;
        //         var l = db.CONGTODIENs.SingleOrDefault(x => x.ID_DIEN == id);
        //         l.CHISODAU = dienchisodau;
        //         l.CHISOCUOI = dienchisocuoi;
        //         l.TRANGTHAI = trangthaidien;
        //         db.CONGTODIENs.Remove(l);
        //         db.SaveChanges();
        //         return Json(new { code = 200, msg = "Xóa danh sách điện thành công!" }, JsonRequestBehavior.AllowGet);
        //     }
        //     catch (Exception e)
        //     {
        //         return Json(new
        //         {
        //             code = 500,
        //             msg = "Xóa danh sách điện thất bại:" + e.Message
        //         }, JsonRequestBehavior.AllowGet);
        //     }



        // }

        // Delete điện
        //[HttpGet]
        // public JsonResult XoaNuoc(int id1, int? nuocchisodau, int? nuocchisocuoi, string trangthainuoc)
        // {
        //     try
        //     {
        //         db.Configuration.ProxyCreationEnabled = false;
        //         var l1 = db.CONGTONUOCs.SingleOrDefault(x => x.ID_NUOC == id1);
        //         l1.CHISODAU = nuocchisodau;
        //         l1.CHISOCUOI = nuocchisocuoi;
        //         l1.TRANGTHAI = trangthainuoc;
        //         db.CONGTONUOCs.Remove(l1);
        //         db.SaveChanges();
        //         return Json(new { code = 200, msg = "Xóa danh sách nước thành công!" }, JsonRequestBehavior.AllowGet);
        //     }
        //     catch (Exception e)
        //     {
        //         return Json(new
        //         {
        //             code = 500,
        //             msg = "Xóa danh sách nước thất bại:" + e.Message
        //         }, JsonRequestBehavior.AllowGet);
        //     }



        // }
    }
}
