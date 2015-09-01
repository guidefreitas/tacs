namespace Tacs.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tacs.Domain.TacsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tacs.Domain.TacsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            var userAdmin = new Models.ApplicationUser();
            userAdmin.Email = "admin@admin.com";
            userAdmin.EmailConfirmed = true;
            userAdmin.UserName = "admin@admin.com";
            userAdmin.PasswordHash = "ANTzcXdfJxks9EOzzs3cfp/0UbtFIJX/b20SX3wu+yfGTZy/UH06FTyTIg5YW5ql9A=="; //admin123
            userAdmin.SecurityStamp = "123";
            context.Users.Add(userAdmin);
            context.SaveChanges();

            var roleAdmin = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            roleAdmin.Name = "Admin";
            context.Roles.Add(roleAdmin);
            context.SaveChanges();
            roleAdmin.Users.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole()
            {
                RoleId = roleAdmin.Id,
                UserId = userAdmin.Id
            });

            context.SaveChanges();
        }
    }
}
