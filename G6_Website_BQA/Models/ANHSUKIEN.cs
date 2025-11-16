using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G6_Website_BQA.Models
{
    public class ANHSUKIEN
    {
        [Key, Column(Order = 0)]
        public int MASK { get; set; }
        public string TENANH { get; set; }
        [Key, Column(Order = 1)]
        public string ANH { get; set; }

        public virtual SUKIEN SUKIEN { get; set; }
    }
}