namespace Tacs.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alternativa",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 200, unicode: false),
                        Correta = c.Boolean(nullable: false),
                        Questao_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questao", t => t.Questao_Id)
                .Index(t => t.Questao_Id);
            
            CreateTable(
                "dbo.Questao",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 200, unicode: false),
                        Dificuldade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Informacao = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AcertoCasual = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Assunto_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assunto", t => t.Assunto_Id)
                .Index(t => t.Assunto_Id);
            
            CreateTable(
                "dbo.Assunto",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teste",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DataInicioValidade = c.DateTime(nullable: false),
                        DataFimValidade = c.DateTime(nullable: false),
                        CriterioInicio = c.Int(nullable: false),
                        CriterioFinalizacao = c.Int(nullable: false),
                        CriterioEscolhaQuestao = c.Int(nullable: false),
                        Disciplina_Id = c.Long(nullable: false),
                        Assunto_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplina", t => t.Disciplina_Id)
                .ForeignKey("dbo.Assunto", t => t.Assunto_Id)
                .Index(t => t.Disciplina_Id)
                .Index(t => t.Assunto_Id);
            
            CreateTable(
                "dbo.Disciplina",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200, unicode: false),
                        Teste_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teste", t => t.Teste_Id)
                .Index(t => t.Teste_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 200, unicode: false),
                        Matricula = c.String(maxLength: 200, unicode: false),
                        TipoUsuario = c.Int(nullable: false),
                        Email = c.String(maxLength: 200, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 200, unicode: false),
                        SecurityStamp = c.String(maxLength: 200, unicode: false),
                        PhoneNumber = c.String(maxLength: 200, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 200, unicode: false),
                        ClaimType = c.String(maxLength: 200, unicode: false),
                        ClaimValue = c.String(maxLength: 200, unicode: false),
                        ApplicationUser_Id = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 200, unicode: false),
                        LoginProvider = c.String(maxLength: 200, unicode: false),
                        ProviderKey = c.String(maxLength: 200, unicode: false),
                        ApplicationUser_Id = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 200, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 200, unicode: false),
                        ApplicationUser_Id = c.String(maxLength: 200, unicode: false),
                        IdentityRole_Id = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.TesteItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TempoResposta = c.DateTime(),
                        Questao_Id = c.Long(nullable: false),
                        Resposta_Id = c.Long(nullable: false),
                        Teste_Id = c.Long(nullable: false),
                        Usuario_Id = c.String(nullable: false, maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questao", t => t.Questao_Id)
                .ForeignKey("dbo.Alternativa", t => t.Resposta_Id)
                .ForeignKey("dbo.Teste", t => t.Teste_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Usuario_Id)
                .Index(t => t.Questao_Id)
                .Index(t => t.Resposta_Id)
                .Index(t => t.Teste_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 200, unicode: false),
                        Name = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserDisciplina",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 200, unicode: false),
                        Disciplina_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Disciplina_Id })
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Disciplina", t => t.Disciplina_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Disciplina_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Alternativa", "Questao_Id", "dbo.Questao");
            DropForeignKey("dbo.Questao", "Assunto_Id", "dbo.Assunto");
            DropForeignKey("dbo.Teste", "Assunto_Id", "dbo.Assunto");
            DropForeignKey("dbo.TesteItem", "Usuario_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.TesteItem", "Teste_Id", "dbo.Teste");
            DropForeignKey("dbo.TesteItem", "Resposta_Id", "dbo.Alternativa");
            DropForeignKey("dbo.TesteItem", "Questao_Id", "dbo.Questao");
            DropForeignKey("dbo.Teste", "Disciplina_Id", "dbo.Disciplina");
            DropForeignKey("dbo.Disciplina", "Teste_Id", "dbo.Teste");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserDisciplina", "Disciplina_Id", "dbo.Disciplina");
            DropForeignKey("dbo.ApplicationUserDisciplina", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.ApplicationUserDisciplina", new[] { "Disciplina_Id" });
            DropIndex("dbo.ApplicationUserDisciplina", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TesteItem", new[] { "Usuario_Id" });
            DropIndex("dbo.TesteItem", new[] { "Teste_Id" });
            DropIndex("dbo.TesteItem", new[] { "Resposta_Id" });
            DropIndex("dbo.TesteItem", new[] { "Questao_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Disciplina", new[] { "Teste_Id" });
            DropIndex("dbo.Teste", new[] { "Assunto_Id" });
            DropIndex("dbo.Teste", new[] { "Disciplina_Id" });
            DropIndex("dbo.Questao", new[] { "Assunto_Id" });
            DropIndex("dbo.Alternativa", new[] { "Questao_Id" });
            DropTable("dbo.ApplicationUserDisciplina");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.TesteItem");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Disciplina");
            DropTable("dbo.Teste");
            DropTable("dbo.Assunto");
            DropTable("dbo.Questao");
            DropTable("dbo.Alternativa");
        }
    }
}
