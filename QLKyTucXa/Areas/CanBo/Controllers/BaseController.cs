using QLKyTucXa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class BaseController : Controller
    {
        // GET: CanBo/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (CanBoLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "CanBo" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}