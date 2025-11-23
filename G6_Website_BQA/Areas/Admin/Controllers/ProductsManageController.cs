using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using G6_Website_BQA.Identity;
using G6_Website_BQA.Models;

namespace G6_Website_BQA.Areas.Admin.Controllers
{
    public class ProductsManageController : Controller
    {
        private DBContext db = new DBContext();

        private void LoadDropdowns(SANPHAM sp = null)
        {
            ViewBag.MALOAISP = new SelectList(db.LOAISPs, "MALOAISP", "TENLOAISP", sp?.MALOAISP);
            ViewBag.MADM = new SelectList(db.DANHMUCs, "MADM", "TENDM", sp?.MADM);
            ViewBag.MATH = new SelectList(db.THUONGHIEUs, "MATH", "TENTH", sp?.MATH);
            ViewBag.MANCC = new SelectList(db.NHACUNGCAPs, "MANCC", "TENNCC", sp?.MANCC);
        }

        public ActionResult Index(string stockFilter = "all")
        {
            var query = db.SANPHAMs.AsQueryable();

            switch (stockFilter)
            {
                case "in":   // còn hàng
                    query = query.Where(p => p.SOLUONG > 0);
                    break;
                case "out":  // hết hàng
                    query = query.Where(p => p.SOLUONG <= 0);
                    break;
                    // "all" thì giữ nguyên
            }

            ViewBag.StockFilter = stockFilter;
            return View(query.ToList());
        }

        public ActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SANPHAM sp)
        {
            if (ModelState.IsValid)
            {
                // đảm bảo số lượng không âm
                if (sp.SOLUONG < 0) sp.SOLUONG = 0;

                db.SANPHAMs.Add(sp);
                db.SaveChanges();

                TempData["Success"] = "Thêm sản phẩm thành công.";
                return RedirectToAction("Index");
            }

            LoadDropdowns(sp);
            return View(sp);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sp = db.SANPHAMs.Find(id);
            if (sp == null)
                return HttpNotFound();

            LoadDropdowns(sp);
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SANPHAM sp)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(sp);
                return View(sp);
            }

            var product = db.SANPHAMs.Find(sp.MASP);
            if (product == null)
                return HttpNotFound();

            product.TENSP = sp.TENSP;
            product.SOLUONG = sp.SOLUONG < 0 ? 0 : sp.SOLUONG;
            product.MOTA = sp.MOTA;
            product.MALOAISP = sp.MALOAISP;
            product.MADM = sp.MADM;
            product.MATH = sp.MATH;
            product.MANCC = sp.MANCC;

            db.SaveChanges();

            TempData["Success"] = "Cập nhật sản phẩm thành công.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var sp = db.SANPHAMs.Find(id);
            if (sp == null)
                return HttpNotFound();

            db.SANPHAMs.Remove(sp);
            db.SaveChanges();

            TempData["Success"] = "Xóa sản phẩm thành công.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}