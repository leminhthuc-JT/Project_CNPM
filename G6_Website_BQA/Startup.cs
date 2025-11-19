using System;
using System.Threading.Tasks;
using G6_Website_BQA.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(G6_Website_BQA.Startup))]

namespace G6_Website_BQA
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Đăng ký AppDBContext và AppUserManager cho mỗi request
            app.CreatePerOwinContext(AppDBContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            this.CreateRolesAndUser();
        }
        public void CreateRolesAndUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new Identity.AppDBContext()));
            var appDBContext = new Identity.AppDBContext();
            var appUserStore = new UserStore<Identity.AppUser>(appDBContext);
            var userManager = new UserManager<Identity.AppUser>(appUserStore);
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (userManager.FindByName("admin") == null)
            {
                var user = new Identity.AppUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                string userPWD = "Admin@123";
                var chkUser = userManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
    }
}
