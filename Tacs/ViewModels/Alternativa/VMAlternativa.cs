using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Helpers;

namespace Tacs.ViewModels.Alternativa
{
    public class VMAlternativa
    {
        public long QuestaoId { get; set; }

        public long Id { get; set; }

        [Required(ErrorMessage ="Informe a descrição da alternativa")]
        [LocalizedDisplayName("TelaAlternativaDescricao")]
        public String Descricao { get; set; }

        [LocalizedDisplayName("TelaAlternativaCorreta")]
        public Boolean Correta { get; set; }
    }
}
