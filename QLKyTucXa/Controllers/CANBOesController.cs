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
    public class CANBOesController : Controller
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: CANBOes
        public ActionResult Index()
        {
            return View(db.CANBOes.ToList());
        }

        // GET: CANBOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CANBO cANBO = db.CANBOes.Find(id);
            if (cANBO == null)
            {
                return HttpNotFound();
            }
            return View(cANBO);
        }

        // GET: CANBOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CANBOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CANBO,MACB,TAIKHOAN,MATKHAU,TENCB,GIOITINH,CMND_CCCD,DIACHI,EMAIL,SDT,QUANTRI,DAXOA")] CANBO cANBO)
        {
            if (ModelState.IsValid)
            {
                db.CANBOes.Add(cANBO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cANBO);
        }

        // GET: CANBOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CANBO cANBO = db.CANBOes.Find(id);
            if (cANBO == null)
            {
                return HttpNotFound();
            }
            return View(cANBO);
        }

        // POST: CANBOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CANBO,MACB,TAIKHOAN,MATKHAU,TENCB,GIOITINH,CMND_CCCD,DIACHI,EMAIL,SDT,QUANTRI,DAXOA")] CANBO cANBO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cANBO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cANBO);
        }

        // GET: CANBOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CANBO cANBO = db.CANBOes.Find(id);
            if (cANBO == null)
            {
                return HttpNotFound();
            }
            return View(cANBO);
        }

        // POST: CANBOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CANBO cANBO = db.CANBOes.Find(id);
            db.CANBOes.Remove(cANBO);
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
