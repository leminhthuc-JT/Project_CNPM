using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G6_Website_BQA.Identity;
using G6_Website_BQA.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace G6_Website_BQA.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;

        public AdminController()
        {
            var db = new AppDBContext();
            userManager = new UserManager<AppUser>(new UserStore<AppUser>(db));
        }
        // GET: Admin
        public ActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        //GET: Admin//CreateUser
        public ActionResult Create()
        {
            return View();
        }

        //POST: Admin//CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string username, string password, string email, string hoten)
        {
            var user = new AppUser
            {
                UserName = username,
                Email = email,
                HoTen = hoten
            };

            var result = userManager.Create(user, password);

            if (result.Succeeded)
                return RedirectToAction("Index");

            ViewBag.Error = string.Join(", ", result.Errors);
            return View();
        }
    }
}