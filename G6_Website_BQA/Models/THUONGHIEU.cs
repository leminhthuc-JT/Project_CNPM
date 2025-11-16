using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G6_Website_BQA.Models
{
    public class THUONGHIEU
    {
        [Key]
        public int MATH { get; set; }
        public string TENTH { get; set; }

        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}