using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Avaliacao
{
    public class VMAvaliacaoPergunta
    {
        public VMAvaliacaoPergunta()
        {
            this.Alternativa = new List<VMAlternativa>();
        }

        public long QuestaoId { get; set; }
        public String Descricao { get; set; }

        public List<VMAlternativa> Alternativa { get; set; }

    }
}
