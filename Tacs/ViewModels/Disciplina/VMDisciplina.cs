using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Helpers;

namespace Tacs.ViewModels.Disciplina
{
    public class VMDisciplina
    {
        public VMDisciplina()
        {
            this.Alunos = new List<VMAluno>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [LocalizedDisplayName("TelaDisciplinaNome")]
        public String Nome { get; set; }


        public List<VMAluno> Alunos { get; set; }
    }
}
