using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Domain.Models;

namespace Tacs.Domain
{
    public class TacsContext : IdentityDbContext<ApplicationUser>
    {
        public TacsContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        public virtual DbSet<Alternativa> Alternativas { get; set; }
        public virtual DbSet<Assunto> Assuntos { get; set; }
        public virtual DbSet<Disciplina> Disciplinas { get; set; }
        public virtual DbSet<Questao> Questoes { get; set; }
        public virtual DbSet<Teste> Testes { get; set; }
        public virtual DbSet<TesteItem> TesteItens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(200));

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public static TacsContext Create()
        {
            return new TacsContext();
        }
    }
}
