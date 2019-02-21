using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PresentationLayer.Models;
using System;

[assembly: OwinStartupAttribute(typeof(PresentationLayer.Startup))]
namespace PresentationLayer
{
    public partial class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRoles();
            //ApplicationDbContext context = new ApplicationDbContext();
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var userbyemail = UserManager.FindByEmail("mck@gmail.com");
            
        }

        public void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Create customer role
                role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);


                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "mayuresh";
                user.Email = "mck@gmail.com";

                string userPWD = "test@123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
          

        }
    }
}
