using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacs.Domain;
using Tacs.Domain.Models;
using Tacs.ViewModels.Teste;
using Tacs.Helpers;
using System.Data.Entity.Validation;

namespace Tacs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TesteController : BaseController
    {
        TacsContext db = new TacsContext();

        [HttpGet]
        public ActionResult Index()
        {
            VMIndexTeste vm = new VMIndexTeste();
            var testes = db.Testes.Select(m => new VMTeste()
            {
                Id = m.Id,
                Assuntos = m.Assuntos.Select(a => a.Titulo).ToList(),
                DisciplinaNome = m.Disciplina.Nome,
                CriterioInicio = m.CriterioInicio,
                CriterioFinalizacao = m.CriterioFinalizacao,
                CriterioEscolhaQuestao = m.CriterioEscolhaQuestao,
                DataInicioValidade = m.DataInicioValidade,
                DataFimValidade = m.DataFimValidade
            }).ToList();

            vm.Testes = testes;
            return View(vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            VMCreateEditTeste vm = new VMCreateEditTeste();
            vm.DataInicioValidade = DateTime.Now;
            vm.DataFimValidade = DateTime.Now.AddMonths(1);
            vm.Disciplinas = db.Disciplinas.Select(m => new VMDisciplina()
            {
                Id = m.Id,
                Nome = m.Nome
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(VMCreateEditTeste vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Teste teste = new Teste();
                    var disciplina = db.Disciplinas.Find(vm.DisciplinaId);
                    teste.Disciplina = disciplina;
                    teste.DataInicioValidade = vm.DataInicioValidade;
                    teste.DataFimValidade = vm.DataFimValidade;
                    teste.CriterioInicio = vm.CriterioInicio;
                    teste.CriterioFinalizacao = vm.CriterioFinalizacao;
                    teste.CriterioEscolhaQuestao = vm.CriterioEscolhaQuestao;
                    db.Testes.Add(teste);
                    db.SaveChanges();
                    this.FlashInfo("Teste criado com sucesso");
                    return RedirectToAction("Edit", "Teste", new { id = teste.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            vm.Disciplinas = db.Disciplinas.Select(m => new VMDisciplina()
            {
                Id = m.Id,
                Nome = m.Nome
            }).ToList();

            return View(vm);
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = db.Testes.Where(m => m.Id == id).FirstOrDefault();
            if (model == null)
                return HttpNotFound();


            VMCreateEditTeste vm = new VMCreateEditTeste();
            vm.Id = model.Id;
            vm.Assuntos = model.Assuntos.Select(a => new VMAssunto()
            {
                Id = a.Id,
                Titulo = a.Titulo
            }).ToList();

            vm.CriterioEscolhaQuestao = model.CriterioEscolhaQuestao;
            vm.CriterioFinalizacao = model.CriterioFinalizacao;
            vm.CriterioInicio = model.CriterioInicio;
            vm.DataFimValidade = model.DataFimValidade;
            vm.DataInicioValidade = model.DataInicioValidade;
            vm.DisciplinaId = model.Disciplina.Id;

            foreach(var item in model.Itens)
            {
                var vmItem = new VMTesteItem();
                vmItem.Id = item.Id;
                vmItem.AlternativaId = item.Resposta.Id;
                vmItem.Questao = item.Questao.Descricao;
                vmItem.QuestaoId = item.Questao.Id;
                vmItem.TesteId = item.Teste.Id;
                vmItem.TempoResposta = item.TempoResposta;
                vmItem.UsuarioNome = item.Usuario.UserName;
                vm.Itens.Add(vmItem);
            }

            /*
            vm.Itens = model.Itens.Select(i => new VMTesteItem()
            {
                Id = i.Id,
                AlternativaId = i.Resposta.Id,
                Questao = i.Questao.Descricao,
                QuestaoId = i.Questao.Id,
                TesteId = i.Teste.Id,
                TempoResposta = i.TempoResposta,
                UsuarioNome = i.Usuario.UserName
            }).ToList();
            */


            vm.Disciplinas = db.Disciplinas.Select(m => new VMDisciplina()
            {
                Id = m.Id,
                Nome = m.Nome
            }).ToList();

            vm.Assuntos = model.Assuntos.Select(m => new VMAssunto()
            {
                Id = m.Id,
                Titulo = m.Titulo
            }).ToList();

            return View(vm);
        }


        [HttpPost]
        public ActionResult Edit(long id, VMCreateEditTeste vm)
        {

            Teste model = db.Testes.Where(m => m.Id == id).FirstOrDefault();
            if (model == null)
                return HttpNotFound();

            if (model.Itens.Count > 0)
            {
                ModelState.AddModelError("", "Esse teste já possui respostas e não pode ser alterado");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var disciplina = db.Disciplinas.Find(vm.DisciplinaId);
                    model.Disciplina = disciplina;
                    model.DataInicioValidade = vm.DataInicioValidade;
                    model.DataFimValidade = vm.DataFimValidade;
                    model.CriterioInicio = vm.CriterioInicio;
                    model.CriterioFinalizacao = vm.CriterioFinalizacao;
                    model.CriterioEscolhaQuestao = vm.CriterioEscolhaQuestao;
                    db.SaveChanges();
                    this.FlashInfo("Teste atualizado com sucesso");
                    return RedirectToAction("Index");
                }catch(DbEntityValidationException dbEx)
                {
                    RegistraErros(dbEx);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            vm.Itens = model.Itens.Select(i => new VMTesteItem()
            {
                Id = i.Id,
                AlternativaId = i.Resposta.Id,
                Questao = i.Questao.Descricao,
                QuestaoId = i.Questao.Id,
                TesteId = i.Teste.Id,
                TempoResposta = i.TempoResposta,
                UsuarioNome = i.Usuario.UserName
            }).ToList();

            vm.Disciplinas = db.Disciplinas.Select(m => new VMDisciplina()
            {
                Id = m.Id,
                Nome = m.Nome
            }).ToList();

            vm.Assuntos = model.Assuntos.Select(m => new VMAssunto()
            {
                Id = m.Id,
                Titulo = m.Titulo
            }).ToList();



            return View(vm);
        }


        [HttpDelete]
        public ActionResult Edit(VMCreateEditTeste vm)
        {
            Teste model = db.Testes.Where(m => m.Id == vm.Id).FirstOrDefault();
            if (model == null)
                return HttpNotFound();

            foreach(var assunto in model.Assuntos.ToList())
            {
                model.Assuntos.Remove(assunto);
            }

            foreach(var item in model.Itens.ToList())
            {
                db.TesteItens.Remove(item);
            }
            db.Testes.Remove(model);
            db.SaveChanges();
            return this.RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult AddAssunto(long id)
        {
            VMAddAssunto vm = new VMAddAssunto();
            vm.Assuntos = db.Assuntos.Select(m => new VMAssunto()
            {
                Id = m.Id,
                Titulo = m.Titulo
            }).ToList();
            vm.TesteId = id;

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddAssunto(long id, VMAddAssunto vm)
        {
            vm.TesteId = id;
            var teste = db.Testes.Where(m => m.Id == id).FirstOrDefault();
            if (teste == null)
                return HttpNotFound();

            var assunto = db.Assuntos.Where(m => m.Id == vm.AssuntoId).FirstOrDefault();
            if (assunto == null)
                return HttpNotFound();

            teste.Assuntos.Add(assunto);
            db.SaveChanges();
            vm.Assuntos = db.Assuntos.Select(m => new VMAssunto()
            {
                Id = m.Id,
                Titulo = m.Titulo
            }).ToList();

            this.FlashInfo("Assunto adicionado com sucesso");
            return RedirectToAction("Edit", new { id = id });
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveAssunto(long id, long assuntoId)
        {
            return View();
        }

    }
}