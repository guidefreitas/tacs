using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Teste
{
    public class VMIndexTeste
    {
        public VMIndexTeste()
        {
            this.Testes = new List<VMTeste>();
        }

        public List<VMTeste> Testes { get; set; }
    }
}
