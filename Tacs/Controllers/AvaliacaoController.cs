using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using IdentitySample.Models;
using Tacs.Domain;
using Tacs.ViewModels.Avaliacao;
using Tacs.ViewModels.Pergunta;
using Tacs.Domain.Models;

namespace Tacs.Controllers
{
    public class AvaliacaoController : Controller
    {
        TacsContext db = new TacsContext();

        // GET: Avaliacao
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (!User.IsInRole("Aluno"))
                return RedirectToAction("NaoAluno");


            var disciplinas = user.Disciplinas;
            VMIndexAvaliacao vm = new VMIndexAvaliacao();

            foreach (var disc in disciplinas)
            {
                foreach(var teste in disc.Testes)
                {
                    VMAvaliacao avaliacao = new VMAvaliacao();
                    avaliacao.Id = teste.Id;
                    avaliacao.DisciplinaNome = teste.Disciplina.Nome;
                    avaliacao.DataInicio = teste.DataInicioValidade;
                    avaliacao.DataFim = teste.DataFimValidade;
                    vm.Avaliacoes.Add(avaliacao);
                }
            }

            return View(vm);
        }

        public ActionResult NaoAluno()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Pergunta(long id)
        {
            var teste = db.Testes.Where(m => m.Id == id).FirstOrDefault();
            if (teste == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(m => m.Id == userId).FirstOrDefault();
            if (user == null)
                return HttpNotFound();

            if (!teste.Disciplina.Alunos.Contains(user))
            {
                return HttpNotFound();
            }

            var assunto = teste.Assuntos.FirstOrDefault();
            if (assunto == null)
                return new HttpStatusCodeResult(500);

            var questoesRespondidasIds = teste.Itens.Where(m => m.Teste.Id == teste.Id && m.Usuario.Id == user.Id).Select(a => a.Id).ToList();

            if(teste.CriterioFinalizacao == Domain.Models.CriterioFinalizacao.ParaNaQuestao30 && questoesRespondidasIds.Count >= 30)
            {
                return RedirectToAction("Concluido", new { id = teste.Id });
            }

            var questao = assunto.Questoes.Where(m => !questoesRespondidasIds.Contains(m.Id)).FirstOrDefault();
            VMQuestao vm = new VMQuestao();
            vm.Descricao = questao.Descricao;
            vm.DataInicioQuestao = DateTime.Now;
            vm.PerguntaId = questao.Id;
            vm.Alternativas = questao.Alternativas.Select(a => new Tacs.ViewModels.Pergunta.VMAlternativa()
            {
                Id = a.Id,
                Descricao = a.Descricao
            }).ToList();

            return View(vm);
        }

        [HttpGet]
        public ActionResult Concluido(long id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pergunta(long id, VMQuestao vm,  FormCollection form)
        {
            Teste teste = db.Testes.Where(m => m.Id == id).FirstOrDefault();
            if (teste == null)
                return new HttpStatusCodeResult(500);

            Questao questao = db.Questoes.Where(m => m.Id == vm.PerguntaId).FirstOrDefault();
            if (questao == null)
                return new HttpStatusCodeResult(500);

            String resposta = form["resposta"];
            long alternativaId = Convert.ToInt64(resposta);
            Alternativa alternativa = questao.Alternativas.Where(m => m.Id == alternativaId).FirstOrDefault();
            if (alternativa == null)
                return new HttpStatusCodeResult(500);

            String userId = User.Identity.GetUserId();
            ApplicationUser usuario = db.Users.Where(m => m.Id == userId).FirstOrDefault();
            if (usuario == null)
                return new HttpStatusCodeResult(500);


            try
            {
                TesteItem testeItem = new TesteItem();
                testeItem.Questao = questao;
                testeItem.Teste = teste;
                testeItem.Resposta = alternativa;
                testeItem.Usuario = usuario;
                testeItem.TempoResposta = DateTime.Now - vm.DataInicioQuestao;
                db.TesteItens.Add(testeItem);
                db.SaveChanges();
                return this.RedirectToAction("Pergunta", new { id = teste.Id });
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }
    }
}