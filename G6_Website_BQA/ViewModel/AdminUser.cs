using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G6_Website_BQA.ViewModel
{
    public class AdminUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string SDT { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string GioiTinh { get; set; }

        public int MALOAIKH { get; set; }
    }
}