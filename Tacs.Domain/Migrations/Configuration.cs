namespace Tacs.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

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

            var roleAluno = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            roleAluno.Name = "Aluno";
            context.Roles.Add(roleAluno);
            context.SaveChanges();


            var roleProfessor = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            roleProfessor.Name = "Professor";
            context.Roles.Add(roleProfessor);
            context.SaveChanges();


            for(int i = 0; i < 10; i++)
            {
                var assunto = new Assunto();
                assunto.Titulo = "Assunto exemplo " + i;
                context.Assuntos.Add(assunto);
                context.SaveChanges();

                for (int j = 0; j < 10; j++)
                {
                    var questao = new Questao();
                    questao.Assunto = assunto;
                    questao.Descricao = "Exemplo de questão " + j;
                    context.Questoes.Add(questao);
                    context.SaveChanges();

                    for(int m = 1; m <= 5; m++)
                    {
                        var alternavia = new Alternativa();
                        alternavia.Descricao = "Alternativa " + m;
                        alternavia.Questao = questao;
                        context.Alternativas.Add(alternavia);
                        context.SaveChanges();
                    }
                }
            }
            
        }
    }
}
