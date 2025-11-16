using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G6_Website_BQA.Models
{
    public class GIAMGIA
    {
        [Key]
        public int MAGG { get; set; }
        public string TENMGG { get; set; }
        public int MUCGIAM { get; set; }
        public string MOTA { get; set; }
        public DateTime NGAYBD { get; set; }
        public DateTime NGAYKT { get; set; }
        public int MALOAISP { get; set; }
        public int MASK { get; set; }
        public virtual ICollection<HOADON> HOADONs { get; set; }
        public virtual LOAISP LOAISP { get; set; }

        public virtual SUKIEN SUKIEN { get; set; }
    }
}