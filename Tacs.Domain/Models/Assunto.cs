using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.Domain.Models
{
    public class Assunto : BaseModel
    {
        [Required]
        public String Titulo { get; set; }

        public virtual ICollection<Questao> Questoes { get; set; }

        public virtual ICollection<Teste> Testes { get; set; }

    }
}
