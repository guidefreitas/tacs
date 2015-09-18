using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacs.Domain.Models;
using Tacs.Domain;
using Tacs.ViewModels.Assunto;
using Tacs.Helpers;

namespace Tacs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AssuntoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            VMIndexAssunto vm = new VMIndexAssunto();
            using (var db = new TacsContext())
            {
                vm.Assuntos = db.Assuntos.Select(m => new VMAssunto()
                {
                    Id = m.Id,
                    Titulo = m.Titulo
                }).ToList();
            }

            return View(vm);
        }


        [HttpGet]
        public ActionResult Create()
        {
            VMAssunto vm = new VMAssunto();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMAssunto vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new TacsContext())
                    {
                        Assunto assunto = new Assunto();
                        assunto.Titulo = vm.Titulo;
                        db.Assuntos.Add(assunto);
                        db.SaveChanges();
                        this.FlashInfo("Assunto cadastrado com sucesso");
                        return this.RedirectToAction("Edit", new { id = assunto.Id });
                    }
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(vm);
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            VMAssunto vm = new VMAssunto();
            using (var db = new TacsContext())
            {
                Assunto assunto = db.Assuntos.Find(id);
                vm.Titulo = assunto.Titulo;
                vm.Id = assunto.Id;
                
                foreach(var questao in assunto.Questoes)
                {
                    vm.Questoes.Add(new ViewModels.Questao.VMQuestao()
                    {
                        Id = questao.Id,
                        Descricao = questao.Descricao,
                        Informacao = questao.Informacao,
                        Dificuldade = questao.Dificuldade,
                        AcertoCasual = questao.AcertoCasual
                    });
                }
            }
                
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMAssunto vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new TacsContext())
                    {
                        Assunto assunto = db.Assuntos.Find(vm.Id);
                        assunto.Titulo = vm.Titulo;
                        db.SaveChanges();
                        this.FlashInfo("Assunto atualizado com sucesso");
                        return this.RedirectToAction("Index");
                    }
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
        public ActionResult Edit(long id, VMAssunto vm)
        {
            try
            {
                using (var db = new TacsContext())
                {
                    Assunto assunto = db.Assuntos.Find(vm.Id);
                    foreach(var questao in assunto.Questoes.ToList())
                    {
                        foreach(var alt in questao.Alternativas.ToList())
                        {
                            db.Alternativas.Remove(alt);
                        }

                        db.Questoes.Remove(questao);
                    }
                    db.Assuntos.Remove(assunto);
                    db.SaveChanges();
                    this.FlashInfo("Assunto removido com sucesso");
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(vm);
        }
    }
}