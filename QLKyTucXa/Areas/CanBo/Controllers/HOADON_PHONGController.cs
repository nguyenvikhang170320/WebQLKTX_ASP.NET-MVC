using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using QLKyTucXa.Models;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class HOADON_PHONGController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();


        [HttpGet]
        public ActionResult DsHDPhong()
        {
            var join = (
                from phong in db.PHONGs
                join hoadon_phong in db.HOADON_PHONG on phong.ID_PHONG equals hoadon_phong.ID_PHONG into tableA
                from tA in tableA.DefaultIfEmpty()
                where (phong.TRANGTHAI == true)
                orderby (tA.NAM) descending
                select new ViewModel_HD()
                {
                    MAPHONG = phong.MAPHONG,
                    MADAYPHONG = phong.DAYPHONG.MADAYPHONG,
                    NAM = tA.NAM,
                    KY = tA.KY,
                    DONGIA = phong.DONGIA,
                    THANHTIEN = phong.DONGIA * 6,
                    TRANGTHAI = tA.TRANGTHAI
                });
            return View(join);
        }
        

        // GET: CanBo/HOADON_PHONG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON_PHONG hOADON_PHONG = db.HOADON_PHONG.Find(id);
            if (hOADON_PHONG == null)
            {
                return HttpNotFound();
            }
            return View(hOADON_PHONG);
        }


        [HttpGet]
        public ActionResult TaoHoaDonPhong()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaoHoaDonPhong(ViewModel_HD viewModel)
        {
            if (ModelState.IsValid)
            {
                dynamic listPhong = (from p in db.PHONGs where p.TRANGTHAI == true select p.ID_PHONG).ToList();
                foreach (var phong in listPhong)
                {
                    HOADON_PHONG data = new HOADON_PHONG()
                    {
                        ID_PHONG = phong,
                        NAM = viewModel.NAM,
                        KY = viewModel.KY,
                        TRANGTHAI = 0
                    };
                    db.HOADON_PHONG.Add(data);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "HOADON_PHONG");
            }
            return View(viewModel);
        }

        // GET: CanBo/HOADON_PHONG/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG");
        //    return View();
        //}

        // POST: CanBo/HOADON_PHONG/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID_HOADON_PHONG,ID_PHONG,NAM,KY,TRANGTHAI")] HOADON_PHONG hOADON_PHONG)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HOADON_PHONG.Add(hOADON_PHONG);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOADON_PHONG.ID_PHONG);
        //    return View(hOADON_PHONG);
        //}

        // GET: CanBo/HOADON_PHONG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON_PHONG hOADON_PHONG = db.HOADON_PHONG.Find(id);
            if (hOADON_PHONG == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOADON_PHONG.ID_PHONG);
            return View(hOADON_PHONG);
        }

        // POST: CanBo/HOADON_PHONG/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_HOADON_PHONG,ID_PHONG,NAM,KY,TRANGTHAI")] HOADON_PHONG hOADON_PHONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON_PHONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOADON_PHONG.ID_PHONG);
            return View(hOADON_PHONG);
        }

        // GET: CanBo/HOADON_PHONG/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON_PHONG hOADON_PHONG = db.HOADON_PHONG.Find(id);
            if (hOADON_PHONG == null)
            {
                return HttpNotFound();
            }
            return View(hOADON_PHONG);
        }

        // POST: CanBo/HOADON_PHONG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOADON_PHONG hOADON_PHONG = db.HOADON_PHONG.Find(id);
            db.HOADON_PHONG.Remove(hOADON_PHONG);
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
