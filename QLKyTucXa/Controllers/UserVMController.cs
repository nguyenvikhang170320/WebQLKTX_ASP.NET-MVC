using Model.EF;
using QLKyTucXa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
{
    public class UserVMController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
        // GET: UserVM
        public ActionResult Index()
        {
            int uid = Convert.ToInt32(Session["idphong"]);
            var join = (from phong in db.PHONGs
                        join hoadon in db.HOADON_DIENNUOC on phong.ID_PHONG equals hoadon.ID_PHONG into tableA
                        from tA in tableA.DefaultIfEmpty()

                        join dien in db.CONGTODIENs on phong.ID_PHONG equals dien.ID_PHONG into tableB
                        from tB in tableB.Where(x=>x.THANG == tA.THANG && x.NAM == tA.NAM).DefaultIfEmpty()

                        join nuoc in db.CONGTONUOCs on phong.ID_PHONG equals nuoc.ID_PHONG into tableC
                        from tC in tableC.Where(x=>x.THANG == tA.THANG && x.NAM == tA.NAM).DefaultIfEmpty()

                        join dongia in db.DONGIAs on tA.ID_DONGIA equals dongia.ID_DONGIA into tableD
                        from tD in tableD.DefaultIfEmpty()

                        where (phong.TRANGTHAI == true && phong.ID_PHONG == uid)
                        select new ViewModel_HD()
                        {
                            MAPHONG = phong.MAPHONG,
                            MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                            DIEN_CHISODAU = tB.CHISODAU,
                            DIEN_CHISOCUOI = tB.CHISOCUOI,
                            CHISODIEN = tB.CHISOCUOI - tB.CHISODAU,
                            NUOC_CHISODAU = tC.CHISODAU,
                            NUOC_CHISOCUOI = tC.CHISOCUOI,
                            CHISONUOC = tC.CHISOCUOI - tC.CHISODAU,
                            THANHTIEN_DIEN = (tB.CHISOCUOI - tB.CHISODAU) * tD.DONGIADIEN,
                            THANHTIEN_NUOC = (tC.CHISOCUOI - tC.CHISODAU) * tD.DONGIANUOC,
                            THANHTIEN = (tB.CHISOCUOI - tB.CHISODAU) * tD.DONGIADIEN + (tC.CHISOCUOI - tC.CHISODAU) * tD.DONGIANUOC,
                            THANG = tA.THANG,
                            NAM = tA.NAM

                        }).ToList().Distinct();
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
            int uid = Convert.ToInt32(Session["idphong"]);
            model.THANG = System.DateTime.Now.Month;
            model.NAM = System.DateTime.Now.Year;
            CONGTONUOC nuoc = new CONGTONUOC()
            {
                ID_PHONG = uid,
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
                ID_PHONG = uid,
                THANG = model.THANG,
                NAM = model.NAM,
                TRANGTHAI = 1,
                CHISODAU = model.DIEN_CHISODAU,
                CHISOCUOI = model.DIEN_CHISOCUOI
            };
            db.CONGTODIENs.Add(dien);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(model);
        }
    }
}