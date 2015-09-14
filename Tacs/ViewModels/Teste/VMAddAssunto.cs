using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Helpers;

namespace Tacs.ViewModels.Teste
{
    public class VMAddAssunto
    {
        public VMAddAssunto()
        {
            this.Assuntos = new List<VMAssunto>();
        }

        public long TesteId { get; set; }

        [Required(ErrorMessage = "Informe um assunto")]
        [LocalizedDisplayName("TelaTesteAddAssunto")]
        public long AssuntoId { get; set; }

        public List<VMAssunto> Assuntos { get; set; }
    }
}
