using G6_Website_BQA.Identity;
using G6_Website_BQA.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace G6_Website_BQA.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Regester()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Regester(Regester re)
        {
            if (ModelState.IsValid)
            {
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);
                var passHash = Crypto.HashPassword(re.Password);
                var user = new AppUser()
                {
                    Email = re.Email,
                    UserName = re.UserName,
                    PasswordHash = passHash,
                    PhoneNumber = re.PhoneNumber,
                    Address = re.Address
                };
                IdentityResult identityResult = userManager.Create(user);
                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                }
                return RedirectToAction("Index", "Home");

            }
            else { return View(); }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login lg)
        {
            if (ModelState.IsValid)
            {
                var appDBContext = new AppDBContext();
                var userStore = new AppUserStore(appDBContext);
                var userManager = new AppUserManager(userStore);
                var user = userManager.Find(lg.UserName, lg.Password);
                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { IsPersistent = false }, userIdentity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}