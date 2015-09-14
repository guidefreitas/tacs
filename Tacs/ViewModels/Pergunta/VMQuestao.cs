using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Pergunta
{
    public class VMQuestao
    {
        public VMQuestao()
        {
            this.Alternativas = new List<VMAlternativa>();
        }
        public long PerguntaId { get; set; }

        public DateTime DataInicioQuestao { get; set; }

        public String Descricao { get; set; }

        public List<VMAlternativa> Alternativas { get; set; }

        public long? AlternativaRespondidaId { get; set; }

    }
}
