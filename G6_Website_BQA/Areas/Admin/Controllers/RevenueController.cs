using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using G6_Website_BQA.Identity;
using G6_Website_BQA.Models;

namespace G6_Website_BQA.Areas.Admin.Controllers
{
    public class RevenueController : Controller
    {
        private DBContext _dbContext;
        public RevenueController()
        {
            _dbContext = new DBContext();
        }
        // GET: Admin/Revenue
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Revenue(DateTime? fromDate, DateTime? toDate)
        {
 
            var ordersQuery = _dbContext.HOADONs.AsQueryable();

            if (fromDate.HasValue)
                ordersQuery = ordersQuery.Where(o => o.NGAYLAP >= fromDate);

            if (toDate.HasValue)
                ordersQuery = ordersQuery.Where(o => o.NGAYLAP <= toDate);

            var ordersList = ordersQuery.ToList(); 

            var totalRevenue = ordersList.Sum(o => (decimal?)o.TONGTIEN) ?? 0m;
            var totalOrders = ordersList.Count();
            var canceledOrders = ordersList.Count(o => o.TRANGTHAI == "ĐÃ HỦY");

            var revenueByDate = ordersList
                 .Where(o => o.NGAYLAP.HasValue)
                 .GroupBy(o => o.NGAYLAP.Value.Date)
                 .Select(g => new
                 {
                     Date = g.Key,
                     TotalRevenue = g.Sum(o => (decimal?)o.TONGTIEN) ?? 0m
                 })
                 .OrderBy(g => g.Date)
                 .ToList();

            var topProducts = _dbContext.CHITIETHDs
                .Join(ordersQuery, c => c.MAHD, h => h.MAHD, (c, h) => new { c, h })
                .GroupBy(x => x.c.MASP)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.c.SOLUONG),
                    Revenue = g.Sum(x => x.c.SOLUONG * x.c.DONGIA)
                })
                .OrderByDescending(p => p.Revenue)
                .Take(5)
                .ToList();
            var averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0m;

            // Truyền dữ liệu vào ViewBag
            ViewBag.AverageOrderValue = averageOrderValue;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.CanceledOrders = canceledOrders;
            ViewBag.RevenueByDate = revenueByDate;
            ViewBag.TopProducts = topProducts;

            // Truyền dữ liệu vào ViewBag cho chart
            ViewBag.RevenueLabels = revenueByDate
                .Select(r => r.Date.ToString("dd-MM"))
                .ToList();
            ViewBag.RevenueData = revenueByDate.Select(r => r.TotalRevenue).ToList();

            return View();
        }
    }
}