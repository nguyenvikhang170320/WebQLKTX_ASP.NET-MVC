using Model.DAO;
using QLKyTucXa.Common;
using QLKyTucXa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKyTucXa.Controllers
{
    public class LoginPhongController : Controller
    {
        // GET: LoginPhong
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult LogoutPhong()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
        public ActionResult LoginPhong(LoginPhongModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new PhongDAO();
                var result = dao.Login(model.TAIKHOAN, Encryptor.MD5Hash(model.MATKHAU));
                if (result == 1)
                {
                    var user = dao.GetById(model.TAIKHOAN);
                    var userSession = new PhongLogin();
                    userSession.TAIKHOAN = user.TAIKHOAN;
                    userSession.UserID = user.ID_PHONG;
                    Session["idphong"] = user.ID_PHONG;//lay thong tin gan vao day
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