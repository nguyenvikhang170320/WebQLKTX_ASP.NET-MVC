using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class HomeController : BaseController
    {
        // GET: CanBo/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}