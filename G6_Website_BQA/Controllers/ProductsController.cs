using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G6_Website_BQA.Identity;
using G6_Website_BQA.Models;

namespace G6_Website_BQA.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            List<SANPHAM> pros = db.SANPHAMs.ToList();
            return View(pros);
        }
    }
}