using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace G6_Website_BQA.Models
{
    public class DANHMUC
    {
        [Key]
        public int MADM { get; set; }
        public string TENDM { get; set; }

        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}