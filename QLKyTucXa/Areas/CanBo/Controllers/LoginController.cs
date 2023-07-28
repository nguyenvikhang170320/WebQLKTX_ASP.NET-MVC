using Model.DAO;
using QLKyTucXa.Areas.CanBo.Models;
using QLKyTucXa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Areas.CanBo.Controllers
{
    public class LoginController : Controller
    {
        // GET: CanBo/Login
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/CanBo/Login");
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CanBoDAO();
                var result = dao.Login(model.TAIKHOAN, Encryptor.MD5Hash(model.MATKHAU));
                if (result == 1)
                {
                    var user = dao.GetById(model.TAIKHOAN);
                    var userSession = new CanBoLogin();
                    userSession.TAIKHOAN = user.TAIKHOAN;
                    userSession.UserID = user.ID_CANBO;
                    Session["idcb"] = user.ID_CANBO;//lay session
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công!");
                }
            }
            return View("Index");
        }
    }
}