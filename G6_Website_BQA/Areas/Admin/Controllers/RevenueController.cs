using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G6_Website_BQA.Models;
using G6_Website_BQA.ViewModel;
using System.Data.Entity;

namespace G6_Website_BQA.Areas.Admin.Controllers
{
    public class RevenueController : Controller
    {
        // GET: Admin/Revenue
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Revenue(DateTime? fromDate, DateTime? toDate)
        {
            using (var db = new DBContext())
            {
                var query = db.HOADONs.AsQueryable();

                if (fromDate.HasValue)
                    query = query.Where(h => h.NGAYLAP >= fromDate);

                if (toDate.HasValue)
                    query = query.Where(h => h.NGAYLAP < toDate.Value.AddDays(1));

                query = query.Where(h => h.TRANGTHAI == "ĐÃ THANH TOÁN");

                var model = new RevenueSummaryViewModel
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    TotalRevenue = query.Sum(h => (decimal?)h.TONGTIEN) ?? 0,
                    TotalOrders = query.Count(),
                    CanceledOrders = db.HOADONs.Count(h => h.TRANGTHAI == "ĐÃ HỦY"),
                };

                model.AverageOrderValue = model.TotalOrders > 0
                    ? model.TotalRevenue / model.TotalOrders
                    : 0;

                model.RevenueByDates = query
                    .GroupBy(h => DbFunctions.TruncateTime(h.NGAYLAP))
                    .Select(g => new RevenueByDateItem
                    {
                        Date = g.Key.Value,
                        Total = g.Sum(x => x.TONGTIEN)
                    }).ToList();

                model.RevenueByDates = query.GroupBy(h => DbFunctions.TruncateTime(h.NGAYLAP)).Select(g => new RevenueByDateItem
                {
                    Date = g.Key.Value,
                    Total = g.Sum(x => x.TONGTIEN)
                }).ToList();
                return View(model);
            }
        }
    }
}