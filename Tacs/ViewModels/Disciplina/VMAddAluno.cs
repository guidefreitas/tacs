using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Helpers;

namespace Tacs.ViewModels.Disciplina
{
    public class VMAddAluno
    {
        public VMAddAluno()
        {
            this.Alunos = new List<VMAluno>();
        }

        [Required(ErrorMessage ="Informe um aluno")]
        [LocalizedDisplayName("TelaAddAlunoAluno")]
        public String AlunoId { get; set; }


        public long DisciplinaId { get; set; }

        public List<VMAluno> Alunos { get; set; }
    }
}
