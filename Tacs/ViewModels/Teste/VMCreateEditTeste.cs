using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Domain.Models;
using Tacs.Helpers;

namespace Tacs.ViewModels.Teste
{
    public class VMCreateEditTeste
    {
        public VMCreateEditTeste()
        {
            this.Assuntos = new List<VMAssunto>();
            this.Itens = new List<VMTesteItem>();
        }

        public long Id { get; set; }

        public List<VMAssunto> Assuntos { get; set; }

        [Required(ErrorMessage ="Informe a disciplina")]
        [LocalizedDisplayName("TelaTesteDisciplina")]
        public long DisciplinaId { get; set; }

        public List<VMDisciplina> Disciplinas { get; set; }

        [Required(ErrorMessage ="Informe a data de início")]
        [LocalizedDisplayName("TelaTesteDataInicioValidade")]
        [DataType(DataType.Text)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DataInicioValidade { get; set; }

        [Required(ErrorMessage = "Informe a data de término de validade")]
        [LocalizedDisplayName("TelaTesteDataFimValidade")]
        [DataType(DataType.Text)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DataFimValidade { get; set; }

        [Required(ErrorMessage = "Informe o critério de início do teste")]
        [LocalizedDisplayName("TelaTesteCriterioInicio")]
        public CriterioInicio CriterioInicio { get; set; }

        [Required(ErrorMessage = "Informe o critério de finalização do teste")]
        [LocalizedDisplayName("TelaTesteCriterioFinalizacao")]
        public CriterioFinalizacao CriterioFinalizacao { get; set; }

        [Required(ErrorMessage = "Informe o critério de escolha de questões")]
        [LocalizedDisplayName("TelaTesteCriterioEscolhaQuestao")]
        public CriterioEscolhaQuestao CriterioEscolhaQuestao { get; set; }

        public List<VMTesteItem> Itens { get; set; }
    }
}
