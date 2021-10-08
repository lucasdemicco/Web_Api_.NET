using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuth.Models;
using WebApiAuth.Repositories;
using static WebApiAuth.Repositories.ITarefa;

namespace WebApiAuth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Read([FromServices]ITarefaRepository repository)
        {
            var tarefas = repository.Read();
            return Ok(tarefas);
               
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.UsuarioId = new Guid(User.Identity.Name);

            repository.Create(model);
            return Ok();
        }


        [HttpPut("{Id}")]
        public IActionResult Update(string Id, [FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            repository.Update(new Guid(Id), model);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(string Id, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            repository.Delete(new Guid(Id));
            return Ok();
        }
    }
}
