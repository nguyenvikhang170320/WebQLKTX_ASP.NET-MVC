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
    public class HOTROesController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: HOTROes
        public ActionResult Index()
        {
            int idphong = Convert.ToInt32(Session["idphong"]);
            var hOTROes = db.HOTROes.Where(p => p.ID_PHONG == idphong).Include(h => h.PHONG);
            return View(hOTROes.ToList());
        }

        // GET: HOTROes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOTRO hOTRO = db.HOTROes.Find(id);
            if (hOTRO == null)
            {
                return HttpNotFound();
            }
            return View(hOTRO);
        }

        // GET: HOTROes/Create
        public ActionResult Create()
        {
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG");
            return View();
        }

        // POST: HOTROes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PHIEU,ID_PHONG,NOIDUNG,NGAYGUI,TRANGTHAI")] HOTRO hOTRO)
        {
            if (ModelState.IsValid)
            {
                int idphong = Convert.ToInt32(Session["idphong"]);
                int trangthai = (hOTRO.TRANGTHAI == 0) ? 0 : 1;
                hOTRO.TRANGTHAI = trangthai;
                hOTRO.NGAYGUI = DateTime.Now;
                hOTRO.ID_PHONG = idphong;
                db.HOTROes.Add(hOTRO);
                db.SaveChanges();
                return RedirectToAction("Index");
                //db.HOTROes.Add(hOTRO);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOTRO.ID_PHONG);
            return View(hOTRO);
        }

        // GET: HOTROes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HOTRO hOTRO = db.HOTROes.Find(id);
        //    if (hOTRO == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOTRO.ID_PHONG);
        //    return View(hOTRO);
        //}

        // POST: HOTROes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID_PHIEU,ID_PHONG,NOIDUNG,NGAYGUI,TRANGTHAI")] HOTRO hOTRO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hOTRO).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ID_PHONG = new SelectList(db.PHONGs, "ID_PHONG", "MAPHONG", hOTRO.ID_PHONG);
        //    return View(hOTRO);
        //}

        // GET: HOTROes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HOTRO hOTRO = db.HOTROes.Find(id);
        //    if (hOTRO == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hOTRO);
        //}

        // POST: HOTROes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    HOTRO hOTRO = db.HOTROes.Find(id);
        //    db.HOTROes.Remove(hOTRO);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
