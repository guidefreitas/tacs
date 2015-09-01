using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.Domain.Models
{
    public class TesteItem : BaseModel
    {
        [Required]
        public virtual Teste Teste { get; set; }

        [Required]
        public virtual Questao Questao { get; set; }

        [Required]
        public virtual ApplicationUser Usuario { get; set; }

        [Required]
        public Alternativa Resposta { get; set; }
        
        public DateTime? TempoResposta { get; set; }
    }
}
