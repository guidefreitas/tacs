using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.Domain.Models
{
    public class Teste : BaseModel, IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.CriterioEscolhaQuestao == CriterioEscolhaQuestao.Aleatoria 
                && this.CriterioFinalizacao == CriterioFinalizacao.AoNivelarConhecimento)
            {
                yield return new ValidationResult("O teste não pode ter escolha aleatória e critério de finalização por nivelamento ao mesmo tempo", new[] { "CriterioEscolhaQuestao", "CriterioFinalizacao" });
            }
        }
    }
}
