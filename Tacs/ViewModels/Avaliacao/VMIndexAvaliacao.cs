using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Avaliacao
{
    public class VMIndexAvaliacao
    {
        public VMIndexAvaliacao()
        {
            this.Avaliacoes = new List<VMAvaliacao>();
        }

        public List<VMAvaliacao> Avaliacoes { get; set; }
    }
}
