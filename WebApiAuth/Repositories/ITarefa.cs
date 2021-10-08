using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuth.Models;

namespace WebApiAuth.Repositories
{
    public class ITarefa
    {
        public interface ITarefaRepository
        {
            List<Tarefa> Read(Guid Id);

            void Create(Tarefa tarefa);
            void Update(Guid Id, Tarefa tarefa);
            void Delete(Guid Id);
        }

        public class TarefaRepository : ITarefaRepository
        {

            private readonly DataContext _context;

            public TarefaRepository(DataContext context)
            {
                _context = context;
            }
            public void Create(Tarefa tarefa)
            {
                tarefa.Id = Guid.NewGuid();
                _context.Add(tarefa);
                _context.SaveChanges();
            }

            public void Delete(Guid Id)
            {
                var tarefa = _context.Tarefas.Find(Id);
                _context.Entry(tarefa).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            public List<Tarefa> Read(Guid Id)
            {
                return _context.Tarefas.Where(tarefa => tarefa.UsuarioId == Id).ToList();
            }

            public void Update(Guid Id, Tarefa tarefa)
            {
                var _tarefa = _context.Tarefas.Find(Id);
                _tarefa.Nome = tarefa.Nome;
                _tarefa.Concluida = tarefa.Concluida;

                _context.Entry(_tarefa).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
