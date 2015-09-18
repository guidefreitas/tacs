using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacs.Domain.Models;
using Tacs.Domain;
using Tacs.ViewModels.Questao;
using Tacs.Helpers;

namespace Tacs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestaoController : Controller
    {

        TacsContext db = new TacsContext();

        [HttpGet]
        public ActionResult Create(long assuntoId)
        {
            Assunto assunto = db.Assuntos.Find(assuntoId);
            VMQuestao vm = new VMQuestao();
            vm.AssuntoId = assunto.Id;
            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMQuestao vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Questao questao = new Questao();
                    Assunto assunto = db.Assuntos.Find(vm.AssuntoId);
                    questao.Assunto = assunto;
                    questao.Descricao = vm.Descricao;
                    questao.Dificuldade = vm.Dificuldade;
                    questao.Informacao = vm.Informacao;
                    questao.AcertoCasual = vm.AcertoCasual;
                    db.Questoes.Add(questao);
                    db.SaveChanges();
                    this.FlashInfo("Questão cadastrada com sucesso");
                    return this.RedirectToAction("Edit", "Questao", new { id = questao.Id });
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(vm);
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            VMQuestao vm = new VMQuestao();
            Questao questao = db.Questoes.Where(m => m.Id == id).FirstOrDefault();
            if (questao == null)
                return HttpNotFound();
            vm.Id = questao.Id;
            vm.AssuntoId = questao.Assunto.Id;
            vm.Descricao = questao.Descricao;
            vm.Dificuldade = questao.Dificuldade;
            vm.Informacao = questao.Informacao;
            vm.AcertoCasual = questao.AcertoCasual;

            foreach (var alternativa in questao.Alternativas)
            {
                vm.Alternativas.Add(new VMAlternativa()
                {
                    Id = alternativa.Id,
                    Descricao = alternativa.Descricao,
                    Correta = alternativa.Correta
                });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMQuestao vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Questao questao = db.Questoes.Find(vm.Id);
                    questao.Descricao = vm.Descricao;
                    questao.Dificuldade = vm.Dificuldade;
                    questao.Informacao = vm.Informacao;
                    questao.AcertoCasual = vm.AcertoCasual;
                    db.SaveChanges();
                    this.FlashInfo("Questão atualizada com sucesso");
                    return this.RedirectToAction("Edit", "Assunto", new { id = questao.Assunto.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(vm);
        }


        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, VMQuestao vm)
        {
            try
            {
                Questao questao = db.Questoes.Find(id);
                foreach(var alt in questao.Alternativas.ToList())
                {
                    db.Alternativas.Remove(alt);
                }
                db.Questoes.Remove(questao);
                db.SaveChanges();
                this.FlashInfo("Questão removida com sucesso");
                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(vm);
        }
    }
}