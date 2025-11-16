using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G6_Website_BQA.Models
{
    public class LOAISP
    {
        [Key]
        public int MALOAISP { get; set; }
        public string TENLOAISP { get; set; }

        public virtual ICollection<GIAMGIA> GIAMGIAs { get; set; }
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}