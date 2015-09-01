using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacs.ViewModels.Disciplina
{
    public class VMIndexDisciplina : VMIndexBase
    {
        public VMIndexDisciplina()
        {
            Disciplinas = new List<VMDisciplina>();
        }

        public List<VMDisciplina> Disciplinas { get; set; }
    }
}
