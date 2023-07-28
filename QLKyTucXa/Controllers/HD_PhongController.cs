using Model.EF;
using QLKyTucXa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
{
    public class HD_PhongController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();
        // GET: HD_Phong
        public ActionResult XemHoaDon()
        {
            int uid = Convert.ToInt32(Session["idphong"]);
            var join = (
                from phong in db.PHONGs
                join hoadon_phong in db.HOADON_PHONG on phong.ID_PHONG equals hoadon_phong.ID_PHONG into tableA
                from tA in tableA.DefaultIfEmpty()
                where (phong.TRANGTHAI == true && phong.ID_PHONG == uid)
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
    }
}