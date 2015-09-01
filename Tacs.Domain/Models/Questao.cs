using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.Domain.Models
{
    public class Questao : BaseModel
    {
        [Required]
        public virtual Assunto Assunto { get; set; }

        [Required]
        public String Descricao { get; set; }

        [Required]
        public Decimal Dificuldade { get; set; }

        [Required]
        public Decimal Informacao { get; set; }

        [Required]
        public Decimal AcertoCasual { get; set; }

        public virtual ICollection<Alternativa> Alternativas { get; set; }
    }
}
