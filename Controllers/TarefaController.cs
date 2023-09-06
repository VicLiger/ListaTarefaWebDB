using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Context;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class TarefaController : Controller
    {

        private readonly TarefaContext _context;

        public TarefaController(TarefaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tarefas = _context.Tarefa.ToList();
            return View(tarefas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                _context.Tarefa.Add(tarefa);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa); 
        }

        public IActionResult Editar(int id)
        {
            var contato = _context.Tarefa.Find(id);

            if(contato == null)
            {
                return NotFound();
            }

            return View(contato);
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefa.Find(tarefa.Id);

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.DataHora = tarefa.DataHora;
            tarefaBanco.Concluida = tarefa.Concluida;

            _context.Tarefa.Update(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var tarefa = _context.Tarefa.Find(id);
            
            if(tarefa == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }


        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefa.Find(id);
            
            _context.Tarefa.Remove(tarefa);
            _context.SaveChanges();

            return View(tarefa);
        }
    }
}