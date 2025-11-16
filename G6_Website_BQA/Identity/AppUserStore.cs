using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace G6_Website_BQA.Identity
{
    public class AppUserStore: UserStore<AppUser>
    {
        public AppUserStore(AppDBContext context) : base(context)
        {
        }
    }
}