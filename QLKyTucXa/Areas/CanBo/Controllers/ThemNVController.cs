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
    public class ThemNVController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CanBo/ThemNV
        public ActionResult Index()
        {
            var lICH_SU = db.LICH_SU.Include(l => l.NHANVIEN).Include(l => l.PHONG);
            return View(lICH_SU.ToList());
        }

        // GET: CanBo/ThemNV/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICH_SU lICH_SU = db.LICH_SU.Find(id);
            if (lICH_SU == null)
            {
                return HttpNotFound();
            }
            return View(lICH_SU);
        }

        // GET: CanBo/ThemNV/Create
        public ActionResult Create()
        {
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV");
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG");
            return View();
        }

        // POST: CanBo/ThemNV/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_NHANVIEN,ID_PHONG,NGAYCHUYEN")] LICH_SU lICH_SU)
        {
            if (ModelState.IsValid)
            {
                db.LICH_SU.Add(lICH_SU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV", lICH_SU.ID_NHANVIEN);
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", lICH_SU.ID_PHONG);
            return View(lICH_SU);
        }

        // GET: CanBo/ThemNV/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICH_SU lICH_SU = db.LICH_SU.Find(id);
            if (lICH_SU == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV", lICH_SU.ID_NHANVIEN);
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", lICH_SU.ID_PHONG);
            return View(lICH_SU);
        }

        // POST: CanBo/ThemNV/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_NHANVIEN,ID_PHONG,NGAYCHUYEN")] LICH_SU lICH_SU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lICH_SU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_NHANVIEN = new SelectList(db.NHANVIENs, "ID_NHANVIEN", "MANV", lICH_SU.ID_NHANVIEN);
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", lICH_SU.ID_PHONG);
            return View(lICH_SU);
        }

        // GET: CanBo/ThemNV/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICH_SU lICH_SU = db.LICH_SU.Find(id);
            if (lICH_SU == null)
            {
                return HttpNotFound();
            }
            return View(lICH_SU);
        }

        // POST: CanBo/ThemNV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LICH_SU lICH_SU = db.LICH_SU.Find(id);
            db.LICH_SU.Remove(lICH_SU);
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
