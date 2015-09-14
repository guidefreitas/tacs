using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Domain.Models;

namespace Tacs.ViewModels.Teste
{
    public class VMTeste
    {
        public VMTeste()
        {
            this.Assuntos = new List<string>();
        }

        public long Id { get; set; }

        public String DisciplinaNome { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime DataFimValidade { get; set; }

        public CriterioInicio CriterioInicio { get; set; }

        public CriterioFinalizacao CriterioFinalizacao { get; set; }

        public CriterioEscolhaQuestao CriterioEscolhaQuestao { get; set; }

        public List<String> Assuntos { get; set; }
    }
}
