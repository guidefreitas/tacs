using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Helpers;

namespace Tacs.ViewModels.Questao
{
    public class VMQuestao
    {
        public VMQuestao()
        {
            this.Alternativas = new List<VMAlternativa>();
        }

        public long Id { get; set; }

        public long AssuntoId { get; set; }

        [Required(ErrorMessage = "Informe a descrição")]
        [LocalizedDisplayName("TelaQuestaoDescricao")]
        public String Descricao { get; set; }

        [LocalizedDisplayName("TelaQuestaoFatorDificuldade")]
        public Decimal Dificuldade { get; set; }

        [LocalizedDisplayName("TelaQuestaoFatorInformação")]
        public Decimal Informacao { get; set; }

        [LocalizedDisplayName("TelaQuestaoFatorAcertoCasual")]
        public Decimal AcertoCasual { get; set; }

        public List<VMAlternativa> Alternativas { get; set; }
    }
}
