using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Questao
{
    public class VMAlternativa
    {
        public long Id { get; set; }
        public String Descricao { get; set; }
        public Boolean Correta { get; set; }
    }
}
