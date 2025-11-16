using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G6_Website_BQA.Identity;
using G6_Website_BQA.Models;

namespace G6_Website_BQA.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            List<ANHSANPHAM> pros = db.ANHSANPHAMs.ToList();
            return View(pros);
        }
        public ActionResult ProFile(string Name = "")
        {
            AppDBContext profile = new AppDBContext();
            AppUser user = profile.Users.SingleOrDefault(r => r.UserName == Name);
            return View(user);
        }
    }
}