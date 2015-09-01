using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacs.Domain.Models;
using Tacs.Domain;
using Tacs.ViewModels.Disciplina;
using Tacs.Helpers;

namespace Tacs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DisciplinaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            VMIndexDisciplina vm = new VMIndexDisciplina();
            using (var db = new TacsContext())
            {
                vm.Disciplinas = db.Disciplinas.Select(m => new VMDisciplina()
                {
                    Id = m.Id,
                    Nome = m.Nome
                }).ToList();
            }

            return View(vm);
        }


        [HttpGet]
        public ActionResult Create()
        {
            VMDisciplina vm = new VMDisciplina();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMDisciplina vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new TacsContext())
                    {
                        Disciplina disciplina = new Disciplina();
                        disciplina.Nome = vm.Nome;
                        db.Disciplinas.Add(disciplina);
                        db.SaveChanges();
                        this.FlashInfo("Disciplina cadastrada com sucesso");
                        return this.RedirectToAction("Edit", new { id = disciplina.Id });
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
            VMDisciplina vm = new VMDisciplina();
            using (var db = new TacsContext())
            {
                Disciplina disciplina = db.Disciplinas.Find(id);
                vm.Nome = disciplina.Nome;
                vm.Id = disciplina.Id;
                foreach(var aluno in disciplina.Alunos)
                {
                    vm.Alunos.Add(new VMAluno()
                    {
                        Id = aluno.Id,
                        Email = aluno.Email,
                        Nome = aluno.UserName
                    });
                }
            }
                
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMDisciplina vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new TacsContext())
                    {
                        Disciplina disciplina = db.Disciplinas.Find(vm.Id);
                        disciplina.Nome = vm.Nome;
                        db.SaveChanges();
                        this.FlashInfo("Disciplina atualizada com sucesso");
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
        public ActionResult Edit(long id, VMDisciplina vm)
        {
            try
            {
                using (var db = new TacsContext())
                {
                    Disciplina disciplina = db.Disciplinas.Find(vm.Id);
                    db.Disciplinas.Remove(disciplina);
                    db.SaveChanges();
                    this.FlashInfo("Disciplina removida com sucesso");
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(vm);
        }


        [HttpGet]
        public ActionResult AddAluno(long id)
        {
            VMAddAluno vm = new VMAddAluno();
            vm.DisciplinaId = id;
            using(var db = new TacsContext())
            {
                var alunos = db.Users.Where(u => u.TipoUsuario == TipoUsuario.Aluno);
                foreach(var aluno in alunos)
                {
                    vm.Alunos.Add(new VMAluno()
                    {
                        Id = aluno.Id,
                        Email = aluno.Email,
                        Nome = aluno.UserName
                    });
                }
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAluno(long id, VMAddAluno vm)
        {
            vm.DisciplinaId = id;
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new TacsContext())
                    {
                        var disciplina = db.Disciplinas.Find(id);
                        if (disciplina == null)
                            return new HttpNotFoundResult();

                        var aluno = db.Users.Where(u => u.TipoUsuario == TipoUsuario.Aluno && u.Id == vm.AlunoId).FirstOrDefault();
                        if (aluno == null)
                            return new HttpNotFoundResult();

                        if (disciplina.Alunos.Contains(aluno))
                        {
                            ModelState.AddModelError("AlunoId", "Aluno já adicionado nessa disciplina");
                            return View(vm);
                        }

                        disciplina.Alunos.Add(aluno);
                        db.SaveChanges();
                        this.FlashInfo("Aluno adicionado com sucesso");
                        return RedirectToAction("Edit", new { id = id });
                    }
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                
            }

            return View(vm);
            
        }

        [HttpGet]
        public ActionResult DeleteAluno(long id, string alunoId)
        {
            using (var db = new TacsContext())
            {
                var aluno = db.Users.Where(u => u.Id == alunoId).FirstOrDefault();
                if (aluno == null)
                    return new HttpNotFoundResult();

                VMDeleteAluno vm = new VMDeleteAluno();
                vm.AlunoId = aluno.Id;
                vm.DisciplinaId = id;
                return View(vm);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAluno(long id, VMDeleteAluno vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new TacsContext())
                    {
                        var aluno = db.Users.Where(u => u.Id == vm.AlunoId).FirstOrDefault();
                        if (aluno == null)
                            return new HttpNotFoundResult();

                        var disciplina = db.Disciplinas.Where(u => u.Id == id).FirstOrDefault();
                        if (disciplina == null)
                            return new HttpNotFoundResult();

                        disciplina.Alunos.Remove(aluno);
                        db.SaveChanges();
                        this.FlashInfo("Aluno removido com sucesso");
                        return RedirectToAction("Edit", new { id = id });
                    }
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                
            }

            return View(vm);
            
        }


    }
}