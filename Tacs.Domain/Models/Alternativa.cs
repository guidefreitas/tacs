using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.Domain.Models
{
    public class Alternativa : BaseModel
    {
        [Required]
        public virtual Questao Questao { get; set; }

        [Required]
        public String Descricao { get; set; }

        [Required]
        public Boolean Correta { get; set; }
    }
}
