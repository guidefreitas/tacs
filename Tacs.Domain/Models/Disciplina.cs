using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Domain.Models;

namespace Tacs.Domain.Models
{
    public class Disciplina : BaseModel
    {
        [Required]
        public String Nome { get; set; }

        public virtual ICollection<ApplicationUser> Alunos { get; set; }

        public virtual ICollection<Teste> Testes { get; set; }
    }
}
