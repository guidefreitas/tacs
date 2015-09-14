using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.Domain.Models
{
    public class Teste : BaseModel
    {
        public virtual ICollection<Assunto> Assuntos { get; set; }

        [Required]
        public DateTime DataInicioValidade { get; set; }

        [Required]
        public DateTime DataFimValidade { get; set; }

        [Required]
        public virtual Disciplina Disciplina { get; set; }

        [Required]
        public CriterioInicio CriterioInicio { get; set; }

        [Required]
        public CriterioFinalizacao CriterioFinalizacao { get; set; }

        [Required]
        public CriterioEscolhaQuestao CriterioEscolhaQuestao { get; set; }

        public virtual ICollection<TesteItem> Itens { get; set; }
    }
}
