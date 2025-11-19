using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace G6_Website_BQA.Identity
{
    public class AppUserManager: UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,IOwinContext context)
        {
            var manager = new AppUserManager(new AppUserStore(context.Get<AppDBContext>()));

            return manager;
        }
    }
}