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
    public class XemNVController : BaseController
    {
        private QLKyTucXaDbContext db = new QLKyTucXaDbContext();

        // GET: XemNV
        public ActionResult Index()
        {
            int uid = Convert.ToInt32(Session["idphong"]);
            var lICH_SU = db.LICH_SU.Include(l => l.NHANVIEN).Include(l => l.PHONG).Where(x=>x.ID_PHONG == uid);
            return View(lICH_SU.ToList());
        }

    }
}
