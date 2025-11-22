using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G6_Website_BQA.ViewModel
{
    public class RevenueSummaryViewModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int CanceledOrders { get; set; }
        public decimal AverageOrderValue { get; set; }

        public List<RevenueByDateItem> RevenueByDates { get; set; }
        public List<TopProductItem> TopProducts { get; set; }
        public List<OrderItem> Orders { get; set; }
    }
    public class RevenueByDateItem
    {
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }

    public class TopProductItem
    {
        public int MASP { get; set; }
        public string TENSP { get; set; }
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }

    public class OrderItem
    {
        public int MAHD { get; set; }
        public DateTime NGAYLAP { get; set; }
        public string CustomerName { get; set; }
        public decimal TONGTIEN { get; set; }
        public string TRANGTHAI { get; set; }
    }
}