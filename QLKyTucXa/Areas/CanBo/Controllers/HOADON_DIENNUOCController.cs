using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using QLKyTucXa.Models;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class HOADON_DIENNUOCController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CanBo/HOADON_DIENNUOC
        public ActionResult Index()
        {
            var hOADON_DIENNUOC = db.HOADON_DIENNUOC.Include(h => h.CANBO).Include(h => h.DONGIA).Include(h => h.PHONG);
            return View(hOADON_DIENNUOC.ToList());
        }

        // GET: CanBo/HOADON_DIENNUOC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON_DIENNUOC hOADON_DIENNUOC = db.HOADON_DIENNUOC.Find(id);
            if (hOADON_DIENNUOC == null)
            {
                return HttpNotFound();
            }
            return View(hOADON_DIENNUOC);
        }

        // GET: CanBo/HOADON_DIENNUOC/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ID_CANBO = new SelectList(db.CANBOes, "ID_CANBO", "MACB");
        //    ViewBag.ID_DONGIA = new SelectList(db.DONGIAs, "ID_DONGIA", "MADONGIA");
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG");
        //    return View();
        //}

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel_HD VM)
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
                        ID_DONGIA = 3,
                        THANG = VM.THANG,
                        NAM = VM.NAM,

                    };
                    db.HOADON_DIENNUOC.Add(data);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "HOADON_DIENNUOC");
            }
            return View(VM);
        }


        // POST: CanBo/HOADON_DIENNUOC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID_HOADON,ID_CANBO,ID_PHONG,ID_DONGIA,THANG,NAM,TRANGTHAI")] HOADON_DIENNUOC hOADON_DIENNUOC)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HOADON_DIENNUOC.Add(hOADON_DIENNUOC);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ID_CANBO = new SelectList(db.CANBOes, "ID_CANBO", "MACB", hOADON_DIENNUOC.ID_CANBO);
        //    ViewBag.ID_DONGIA = new SelectList(db.DONGIAs, "ID_DONGIA", "MADONGIA", hOADON_DIENNUOC.ID_DONGIA);
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOADON_DIENNUOC.ID_PHONG);
        //    return View(hOADON_DIENNUOC);
        //}

        // GET: CanBo/HOADON_DIENNUOC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON_DIENNUOC hOADON_DIENNUOC = db.HOADON_DIENNUOC.Find(id);
            if (hOADON_DIENNUOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CANBO = new SelectList(db.CANBOes, "ID_CANBO", "MACB", hOADON_DIENNUOC.ID_CANBO);
            ViewBag.ID_DONGIA = new SelectList(db.DONGIAs, "ID_DONGIA", "MADONGIA", hOADON_DIENNUOC.ID_DONGIA);
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOADON_DIENNUOC.ID_PHONG);
            return View(hOADON_DIENNUOC);
        }

        // POST: CanBo/HOADON_DIENNUOC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_HOADON,ID_CANBO,ID_PHONG,ID_DONGIA,THANG,NAM,TRANGTHAI")] HOADON_DIENNUOC hOADON_DIENNUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON_DIENNUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CANBO = new SelectList(db.CANBOes, "ID_CANBO", "MACB", hOADON_DIENNUOC.ID_CANBO);
            ViewBag.ID_DONGIA = new SelectList(db.DONGIAs, "ID_DONGIA", "MADONGIA", hOADON_DIENNUOC.ID_DONGIA);
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOADON_DIENNUOC.ID_PHONG);
            return View(hOADON_DIENNUOC);
        }

        // GET: CanBo/HOADON_DIENNUOC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON_DIENNUOC hOADON_DIENNUOC = db.HOADON_DIENNUOC.Find(id);
            if (hOADON_DIENNUOC == null)
            {
                return HttpNotFound();
            }
            return View(hOADON_DIENNUOC);
        }

        // POST: CanBo/HOADON_DIENNUOC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOADON_DIENNUOC hOADON_DIENNUOC = db.HOADON_DIENNUOC.Find(id);
            db.HOADON_DIENNUOC.Remove(hOADON_DIENNUOC);
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
