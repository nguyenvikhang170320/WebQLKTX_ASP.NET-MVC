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
        [HttpGet]
        public JsonResult LoadDSDN(string tuKhoa, int trang)
        {
            try
            {
                //int uid = Convert.ToInt32(Session["idphong"]);
                var dshdp = (
                      from phong in db.PHONGs.Where(x => x.TRANGTHAI == true && x.DAXOA != true)
                      join dien in db.CONGTODIENs on phong.ID_PHONG equals dien.ID_PHONG into tableA
                      from tA in tableA.DefaultIfEmpty()
                      join nuoc in db.CONGTONUOCs on phong.ID_PHONG equals nuoc.ID_PHONG into tableB
                      from tB in tableB.DefaultIfEmpty()
                      where (phong.MAPHONG.ToLower().Contains(tuKhoa))
                      || phong.DAYPHONG.MADAYPHONG.ToLower().Contains(tuKhoa)
                      select new ViewModel_HDĐN_HDP
                      {
                          ID_PHONG = phong.ID_PHONG,
                          MAPHONG = phong.MAPHONG,
                          MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                          NAM = tA.NAM,
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


        // GET: CanBo/CONGTODIENNUOC/Create
        public ActionResult Create()
        {
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG");
            return View();
        }

        // POST: CanBo/CONGTODIENNUOC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DIEN,ID_PHONG,CHISODAU,CHISOCUOI,THANG,NAM,TRANGTHAI")] CONGTODIEN cONGTODIEN)
        {
            if (ModelState.IsValid)
            {
                db.CONGTODIENs.Add(cONGTODIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", cONGTODIEN.ID_PHONG);
            return View(cONGTODIEN);
        }

        // GET: CanBo/CONGTODIENNUOC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONGTODIEN cONGTODIEN = db.CONGTODIENs.Find(id);
            if (cONGTODIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", cONGTODIEN.ID_PHONG);
            return View(cONGTODIEN);
        }

        // POST: CanBo/CONGTODIENNUOC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DIEN,ID_PHONG,CHISODAU,CHISOCUOI,THANG,NAM,TRANGTHAI")] CONGTODIEN cONGTODIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONGTODIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", cONGTODIEN.ID_PHONG);
            return View(cONGTODIEN);
        }

        // GET: CanBo/CONGTODIENNUOC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONGTODIEN cONGTODIEN = db.CONGTODIENs.Find(id);
            if (cONGTODIEN == null)
            {
                return HttpNotFound();
            }
            return View(cONGTODIEN);
        }

        // POST: CanBo/CONGTODIENNUOC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONGTODIEN cONGTODIEN = db.CONGTODIENs.Find(id);
            db.CONGTODIENs.Remove(cONGTODIEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
