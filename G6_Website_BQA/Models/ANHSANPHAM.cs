using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G6_Website_BQA.Models
{
    public class ANHSANPHAM
    {
        [Key, Column(Order = 0)]
        public int MASP { get; set; }
        [Key, Column(Order = 1)]
        public string ANH { get; set; }
        public string TENANH { get; set; }
        

        public virtual SANPHAM SANPHAM { get; set; }
    }
}