using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLKyTucXa.Models;
using Model.EF;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class AdminVMController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
       
        // GET: CanBo/AdminVM
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HOADON_DIENNUOC hddn)
        {
            int uid = Convert.ToInt32(Session["idcb"]);
            if (ModelState.IsValid)
            {
                dynamic listPhong = (from p in db.PHONGs where p.TRANGTHAI == true select p.ID_PHONG).ToList();
                //List<int> listPhong = (from p in db.PHONGs where p.TRANGTHAI == 1 select p.ID_PHONG).ToList();
                foreach (var phong in listPhong)
                {
                    HOADON_DIENNUOC data = new HOADON_DIENNUOC()
                    {
                        ID_CANBO = uid,
                        ID_PHONG = phong,
                        TRANGTHAI = 0,
                        ID_DONGIA = 2,
                        THANG = DateTime.Now.Month,
                        NAM = DateTime.Now.Year

                    };
                    db.HOADON_DIENNUOC.Add(data);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "HOADON_DIENNUOC");
            }
            return View(hddn);
        }
        public ActionResult Index()
        {
            var join = (from phong in db.PHONGs
                        join dien in db.CONGTODIENs on phong.ID_PHONG equals dien.ID_PHONG into dien1
                        from d in dien1.DefaultIfEmpty()

                        join nuoc in db.CONGTONUOCs on phong.ID_PHONG equals nuoc.ID_PHONG into nuoc1
                        from n in nuoc1.DefaultIfEmpty()

                        join hoadon in db.HOADON_DIENNUOC on phong.ID_PHONG equals hoadon.ID_PHONG into hoadon1
                        from h in hoadon1.DefaultIfEmpty()

                        join dongia in db.DONGIAs on h.ID_DONGIA equals dongia.ID_DONGIA into dongia1
                        from d1 in dongia1.DefaultIfEmpty()

                        where (phong.ID_PHONG == h.ID_PHONG)
                        select new ViewModel_HD()
                        {
                            MAPHONG = phong.MAPHONG,
                            MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                            DIEN_CHISODAU = d.CHISODAU,
                            DIEN_CHISOCUOI = d.CHISOCUOI,
                            CHISODIEN = d.CHISOCUOI - d.CHISODAU,
                            NUOC_CHISODAU = n.CHISODAU,
                            NUOC_CHISOCUOI = n.CHISOCUOI,
                            CHISONUOC = n.CHISOCUOI - n.CHISODAU,
                            THANHTIEN_DIEN = (d.CHISOCUOI - d.CHISODAU) * d1.DONGIADIEN,
                            THANHTIEN_NUOC = (n.CHISOCUOI - n.CHISODAU) * d1.DONGIANUOC,
                            THANHTIEN = (d.CHISOCUOI - d.CHISODAU) * d1.DONGIADIEN + (n.CHISOCUOI - n.CHISODAU) * d1.DONGIANUOC,
                            THANG = h.THANG,
                            NAM = h.NAM

                        }).ToList();
                     return View(join);

        }

        [HttpGet]
        public ActionResult CreateDIEN_NUOC()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDIEN_NUOC(ViewModel_HD model)
        {
            model.THANG = System.DateTime.Now.Month;
            model.NAM = System.DateTime.Now.Year;
            CONGTONUOC nuoc = new CONGTONUOC()
            {
                ID_PHONG = model.ID_PHONG,
                THANG = model.THANG,
                NAM = model.NAM,
                TRANGTHAI = 1,
                CHISODAU = model.NUOC_CHISODAU,
                CHISOCUOI = model.NUOC_CHISOCUOI
            };
            db.CONGTONUOCs.Add(nuoc);

            // dien
            CONGTODIEN dien = new CONGTODIEN()
            {
                ID_PHONG = model.ID_PHONG,
                THANG = model.THANG,
                NAM = model.NAM,
                TRANGTHAI = 1,
                CHISODAU = model.DIEN_CHISODAU,
                CHISOCUOI = model.DIEN_CHISOCUOI
            };
            db.CONGTODIENs.Add(dien);
            db.SaveChanges();
            return View(model);
        }


        // Tạo hóa đơn phòng

    }
}