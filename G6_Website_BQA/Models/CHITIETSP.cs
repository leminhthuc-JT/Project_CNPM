using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G6_Website_BQA.Models
{
    public class CHITIETSP
    {
        [Key, Column(Order = 0)]
        public int MASP { get; set; }
        [Key, Column(Order = 1)]
        public string MAU { get; set; }
        [Key, Column(Order = 2)]
        public string KICHTHUOC { get; set; }
        public decimal DONGIA { get; set; }
        public int SOLUONG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}