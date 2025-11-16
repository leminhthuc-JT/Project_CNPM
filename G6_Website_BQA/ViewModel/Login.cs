using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G6_Website_BQA.ViewModel
{
    public class Login
    {

        [Required(ErrorMessage = "Không được để trống")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}