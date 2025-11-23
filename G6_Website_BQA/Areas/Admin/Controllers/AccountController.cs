using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using G6_Website_BQA.Identity;
using G6_Website_BQA.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace G6_Website_BQA.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager _userManager;
        private AppDBContext _dbContext;

        public AccountController()
        {
            _dbContext = new AppDBContext();
            _userManager = new AppUserManager(new AppUserStore(_dbContext));
        }

        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult AccountManage()
        {
            var users = UserManager.Users.ToList();

            return View(users);
        }

        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(new[] { "Admin", "Customer" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string userName, string email, string hoTen, string password, string role)
        {
            if (ModelState.IsValid)
            {
                // Tạo mới tài khoản user
                var user = new AppUser
                {
                    UserName = userName,
                    Email = email,
                    HoTen = hoTen
                };

                // Tạo tài khoản với password
                var result = UserManager.Create(user, password);

                // Nếu tài khoản được tạo thành công, gán role cho user
                if (result.Succeeded)
                {
                    // Gán quyền role cho người dùng
                    AssignRoleToUser(user.Id, role);

                    TempData["Success"] = "Tạo tài khoản thành công.";
                    return RedirectToAction("AccountManage");
                }

                // Nếu có lỗi khi tạo tài khoản, hiển thị lỗi
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View();
        }
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
        // Hàm gán quyền (role) cho user
        private void AssignRoleToUser(string userId, string role)
        {
            var user = _dbContext.Users.Find(userId);
            if (user != null)
            {
                // Gán role cho user
                user.Role = role;
                _dbContext.SaveChanges();
            }
        }

        private string GetRole(string userId)
        {
            var user = _dbContext.Users.Find(userId);
            return user?.Role ?? "User";
        }

    }
}