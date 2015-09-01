using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacs.Domain;
using Tacs.Domain.Models;
using Tacs.ViewModels.Alternativa;
using Tacs.Helpers;

namespace Tacs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AlternativaController : Controller
    {
        TacsContext db = new TacsContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(long questaoId)
        {
            VMAlternativa vm = new VMAlternativa();
            vm.QuestaoId = questaoId;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(long questaoId, VMAlternativa vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var questao = db.Questoes.Where(m => m.Id == questaoId).FirstOrDefault();
                    if (questao == null)
                        return HttpNotFound();

                    Alternativa alternativa = new Alternativa();
                    alternativa.Descricao = vm.Descricao;
                    alternativa.Correta = vm.Correta;
                    alternativa.Questao = questao;
                    db.Alternativas.Add(alternativa);
                    db.SaveChanges();
                    this.FlashInfo("Alternativa cadastrada com sucesso");
                    return this.RedirectToAction("Edit", "Questao", new { id = questaoId });
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
            VMAlternativa vm = new VMAlternativa();
            var alternativa = db.Alternativas.Where(m => m.Id == id).FirstOrDefault();
            if (alternativa == null)
                return HttpNotFound();
            vm.Id = alternativa.Id;
            vm.QuestaoId = alternativa.Questao.Id;
            vm.Descricao = alternativa.Descricao;
            vm.Correta = alternativa.Correta;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long id, VMAlternativa vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Alternativa alternativa = db.Alternativas.Where(m => m.Id == vm.Id).FirstOrDefault();
                    if (alternativa == null)
                        return HttpNotFound();

                    alternativa.Descricao = vm.Descricao;
                    alternativa.Correta = vm.Correta;
                    db.SaveChanges();
                    this.FlashInfo("Alternativa cadastrada com sucesso");
                    return this.RedirectToAction("Edit", "Questao", new { id = alternativa.Questao.Id });
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
        public ActionResult Edit(VMAlternativa vm)
        {
            Alternativa alternativa = db.Alternativas.Where(m => m.Id == vm.Id).FirstOrDefault();
            
            if (alternativa == null)
                return HttpNotFound();

            var questaoId = alternativa.Questao.Id;
            db.Alternativas.Remove(alternativa);
            db.SaveChanges();
            return this.RedirectToAction("Edit", "Questao", new { id = questaoId });
        }

    }
}