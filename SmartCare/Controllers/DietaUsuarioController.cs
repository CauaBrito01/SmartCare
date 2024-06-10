using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCare.Connection;
using SmartCare.Interfaces;
using SmartCare.Models;


namespace SmartCare.Controllers
{
    [Route("api/dietausuario")]
    [ApiController]
    public class DietaUsuarioController : ControllerBase
    {
        private readonly IDietaRepository _repository;

        public DietaUsuarioController(IDietaRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        [HttpGet]
        public IActionResult ListarDietas()
        {
            var dietas = _repository.List();
            if (!dietas.Any())
            {
                return NotFound("Sem dietas disponíveis");
            }

            return Ok(dietas);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            var dieta = _repository.Find(id);
            if (dieta == null)
            {
                return NotFound("Dieta não encontrada");
            }

            return Ok(dieta);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(DietaUsuarioModel model)
        {
            _repository.Add(model);
            return Ok("Dieta Criada");
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DietaUsuarioModel model)
        {
            var dieta = _repository.Find(id);
            if (dieta == null)
            {
                return NotFound($"Dieta com o ID {id} não encontrada");
            }

            // Atualiza os atributos da dieta com base nos dados recebidos no corpo da solicitação
            if (model.TITULO_DIETA != null)
            {
                dieta.TITULO_DIETA = model.TITULO_DIETA;
            }
            if (model.DESCRICAO_DIETA != null)
            {
                dieta.DESCRICAO_DIETA = model.DESCRICAO_DIETA;
            }    
            if (model.HORA_DIETA != null)
            {
                dieta.HORA_DIETA = model.HORA_DIETA;
            }

            _repository.Put(dieta);
            return Ok("Dieta editada");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok("Dieta deletada");
        }
    }
}