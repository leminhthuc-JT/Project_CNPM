using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace G6_Website_BQA.Identity
{
    public class AppUser: IdentityUser
    {
        public string Address { get; set; }
    }
}