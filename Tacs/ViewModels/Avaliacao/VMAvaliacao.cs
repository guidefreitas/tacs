using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Avaliacao
{
    public class VMAvaliacao
    {
        public long Id { get; set; }

        public String DisciplinaNome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
