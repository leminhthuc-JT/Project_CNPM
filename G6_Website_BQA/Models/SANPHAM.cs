using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G6_Website_BQA.Models
{
    public class SANPHAM
    {
        [Key]
        public int MASP { get; set; }
        public string TENSP { get; set; }
        public int SOLUONG { get; set; }
        public string MOTA { get; set; }
        public int MALOAISP { get; set; }
        public int MADM { get; set; }
        public int MATH { get; set; }
        public int MANCC { get; set; }

        public virtual ICollection<ANHSANPHAM> ANHSANPHAMs { get; set; }
        public virtual ICollection<CHITIETHD> CHITIETHDs { get; set; }
        public virtual ICollection<CHITIETSP> CHITIETSPs { get; set; }
        public virtual DANHMUC DANHMUC { get; set; }
        public virtual LOAISP LOAISP { get; set; }
        public virtual NHACUNGCAP NHACUNGCAP { get; set; }
        public virtual THUONGHIEU THUONGHIEU { get; set; }
    }
}