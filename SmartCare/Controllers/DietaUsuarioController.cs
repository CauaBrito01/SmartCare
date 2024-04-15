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

        [HttpGet]
        public IActionResult ListarDietas()
        {
            var dietas = _repository.ListarDietas();
            if (!dietas.Any())
            {
                return NotFound("Sem dietas disponíveis");
            }

            return Ok(dietas);
        }

        [HttpGet("{id}")]
        public IActionResult ListaDieta(int id)
        {
            var dieta = _repository.ListaDieta(id);
            if (dieta == null)
            {
                return NotFound("Dieta não encontrada");
            }

            return Ok(dieta);
        }

        [HttpPost]
        public IActionResult GravarDieta(DietaUsuarioModel model)
        {
            _repository.GravarDieta(model);
            return Ok("Dieta Criada");
        }

        [HttpPut]
        public IActionResult EditarDieta(DietaUsuarioModel model)
        {
            if (model == null)
            {
                return BadRequest("Modelo de dados inválido");
            }

            var dieta = _repository.ListaDieta(model.ID_DIETA);
            if (dieta == null)
            {
                return NotFound($"Dieta com o Id {model.ID_DIETA} não encontrada");
            }

            _repository.EditarDieta(model);
            return Ok("Dieta Editada");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarDieta(int id)
        {
            _repository.DeletarDieta(id);
            return Ok("Dieta deletada");
        }
    }
}