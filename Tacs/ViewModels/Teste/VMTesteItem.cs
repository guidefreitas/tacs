using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Teste
{
    public class VMTesteItem
    {

        public long Id { get; set; }

        public long TesteId { get; set; }

        public long QuestaoId { get; set; }

        public String Questao { get; set; }

        public String UsuarioNome { get; set; }

        public long AlternativaId { get; set; }

        public TimeSpan? TempoResposta { get; set; }
    }
}
