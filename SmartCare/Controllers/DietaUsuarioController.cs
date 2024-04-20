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
            var dietas = _repository.List();
            if (!dietas.Any())
            {
                return NotFound("Sem dietas disponíveis");
            }

            return Ok(dietas);
        }

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

        [HttpPost]
        public IActionResult Add(DietaUsuarioModel model)
        {
            _repository.Add(model);
            return Ok("Dieta Criada");
        }

        [HttpPut]
        public IActionResult Put(DietaUsuarioModel model)
        {
            if (model == null)
            {
                return BadRequest("Modelo de dados inválido");
            }

            var dieta = _repository.Find(model.ID_DIETA);
            if (dieta == null)
            {
                return NotFound($"Dieta com o Id {model.ID_DIETA} não encontrada");
            }

            _repository.Put(model);
            return Ok("Dieta Editada");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok("Dieta deletada");
        }
    }
}