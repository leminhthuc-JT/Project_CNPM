using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G6_Website_BQA.Models
{
    public class HOADON
    {
        [Key]
        public int MAHD { get; set; }
        //public int MATK { get; set; }
        public Nullable<System.DateTime> NGAYLAP { get; set; }
        public int MAGG { get; set; }
        public decimal TONGTIEN { get; set; }
        public string PTTHANHTOAN { get; set; }
        public string TRANGTHAI { get; set; }
        public virtual GIAMGIA GIAMGIA { get; set; }
        public virtual ICollection<CHITIETHD> CHITIETHDs { get; set; }
        //public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}