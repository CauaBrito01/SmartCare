using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.Interfaces;
using SmartCare.Models;

namespace SmartCare.Controllers
{
    [Route("api/calendario")]
    [ApiController]
    public class CalendarioController : ControllerBase
    {
        private readonly ICalendarioRepository _repository;

        public CalendarioController(ICalendarioRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        [HttpGet]
        public IActionResult List()
        {
            var calendario = _repository.List();
            if (!calendario.Any())
            {
                return NotFound("Sem atividades disponíveis");
            }

            return Ok(calendario);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            var calendario = _repository.Find(id);
            if (calendario == null)
            {
                return NotFound("atividade não encontrada");
            }

            return Ok(calendario);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(CalendarioModel model)
        {
            _repository.Add(model);
            return Ok("Atividade agendada no calendario");
        }
        [Authorize]
        [HttpPut]
        public IActionResult Put(CalendarioModel model)
        {
            if (model == null)
            {
                return BadRequest("Modelo de dados inválido");
            }

            var calendario = _repository.Find(model.ID_CALENDARIO);
            if (calendario == null)
            {
                return NotFound($"Atividade com o Id {model.ID_CALENDARIO} não encontrada");
            }

            _repository.Put(model);
            return Ok("Atividade Editada");
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok("Atividade deletada");
        }
    }
}
