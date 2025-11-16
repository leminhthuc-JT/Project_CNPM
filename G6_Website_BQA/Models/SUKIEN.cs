using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G6_Website_BQA.Models
{
    public class SUKIEN
    {
        [Key]
        public int MASK { get; set; }
        public string TENSK { get; set; }
        public DateTime NGAYBD { get; set; }
        public DateTime NGAYKT { get; set; }

        public virtual ICollection<ANHSUKIEN> ANHSUKIENs { get; set; }
        public virtual ICollection<GIAMGIA> GIAMGIAs { get; set; }
    }
}