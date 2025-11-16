using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G6_Website_BQA.Models
{
    public class CHITIETHD
    {
        [Key, Column(Order = 0)]
        public int MAHD { get; set; }
        [Key, Column(Order = 1)]
        public int MASP { get; set; }
        public int SOLUONG { get; set; }
        public decimal DONGIA { get; set; }

        public virtual HOADON HOADON { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}