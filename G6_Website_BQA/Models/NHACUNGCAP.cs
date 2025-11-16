using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G6_Website_BQA.Models
{
    public class NHACUNGCAP
    {
        [Key]
        public int MANCC { get; set; }
        public string TENNCC { get; set; }
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}