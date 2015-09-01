using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Assunto
{
    public class VMIndexAssunto : VMIndexBase
    {
        public VMIndexAssunto()
        {
            Assuntos = new List<VMAssunto>();
        }

        public List<VMAssunto> Assuntos { get; set; }
    }
}
