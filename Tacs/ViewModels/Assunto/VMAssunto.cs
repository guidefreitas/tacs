using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Helpers;
using Tacs.ViewModels.Questao;

namespace Tacs.ViewModels.Assunto
{
    public class VMAssunto
    {
        public VMAssunto()
        {
            this.Questoes = new List<VMQuestao>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Informe o assunto")]
        [LocalizedDisplayName("TelaAssuntoTitulo")]
        public String Titulo { get; set; }

        public List<VMQuestao> Questoes { get; set; }
    }
}
