using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using G6_Website_BQA.Identity;
using G6_Website_BQA.Models;
using G6_Website_BQA.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace G6_Website_BQA.Controllers
{
    public class AdminController : Controller
    {
        private AppUserManager _userManager;

        /// <summary>
        /// Lấy AppUserManager từ OWIN cho mỗi request
        /// </summary>
        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Admin/AccountManage
        public ActionResult AccountManage()
        {
            // Dùng AppUserManager từ OWIN
            var users = UserManager.Users.ToList();

            var model = users.Select(u => new AccountViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                HoTen = u.HoTen,
                Role = UserManager.GetRoles(u.Id).FirstOrDefault() ?? "User"
            }).ToList();

            return View(model);
        }

        // GET: /Admin/Create
        public ActionResult Create()
        {
            var model = new CreateAccountViewModel();
            return View(model);
        }

        // POST: /Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                HoTen = model.HoTen
            };

            var result = UserManager.Create(user, model.Password);

            if (result.Succeeded)
            {
                // Gán role nếu có chọn và role đó đã được tạo trong Startup (Admin / Customer)
                if (!string.IsNullOrEmpty(model.Role))
                {
                    UserManager.AddToRole(user.Id, model.Role);
                }

                TempData["Success"] = "Tạo tài khoản thành công.";
                return RedirectToAction("AccountManage");
            }

            // Nếu có lỗi từ Identity (trùng username, email, pass yếu...)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(model);
        }

        // POST: /Admin/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // không cho tự xóa chính mình
            if (User.Identity.GetUserId() == id)
            {
                TempData["Error"] = "Không thể tự xóa tài khoản đang đăng nhập.";
                return RedirectToAction("AccountManage");
            }

            var result = UserManager.Delete(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "Xóa tài khoản thành công.";
            }
            else
            {
                TempData["Error"] = string.Join(", ", result.Errors);
            }

            return RedirectToAction("AccountManage");
        }
    }
}