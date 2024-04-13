using Microsoft.AspNetCore.Mvc;
using SmartCare.Connection;
using SmartCare.Models;


namespace SmartCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietaUsuarioController : ControllerBase
    {
        private readonly ConnectionDb _context;

        public DietaUsuarioController(ConnectionDb context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult ListarDietas()
        {
            var dietas = _context.DIETA_USUARIO.ToList();
            if (dietas.Count == 0 )
            {
                return NotFound("Sem diestas disponiveis");
            }

            return Ok(dietas);
        }

        [HttpGet("{id}")]
        public IActionResult ListaDieta(int id)
        {
            var Dieta = _context.DIETA_USUARIO.Find(id);
            if (Dieta == null)
            {
                return NotFound("Diesta não encontrada");
            }

            return Ok(Dieta);
        }


        [HttpPost]
        public IActionResult GravarDieta(DietaUsuarioModel model)
        {
            
            _context.Add(model);
            _context.SaveChanges();
            return Ok("Dieta Criada");
            
            
        }

        [HttpPut]
        public IActionResult EditarDieta(DietaUsuarioModel model)
        {
            if (model == null || model.ID_DIETA == 0){
                if (model == null)
                {
                    return BadRequest("Model Data não e valide");
                }

                else if (model.ID_DIETA == 0)
                {
                    return BadRequest($" Id {model.ID_DIETA} não é um id valido");
                }
            }
            var dieta = _context.DIETA_USUARIO.Find(model.ID_DIETA);
            if (dieta == null)
            {
                return NotFound($" Dieta com o Id {model.ID_DIETA} não encontrada");
            }
            dieta.TITULO_DIETA = model.TITULO_DIETA;
            dieta.DESCRICAO_DIETA = model.DESCRICAO_DIETA;
            dieta.HORA_DIETA=model.HORA_DIETA;
            dieta.ID_USUARIO = model.ID_USUARIO;
            
            _context.SaveChanges();
            
            return Ok("Dieta Editada");
        }

        [HttpDelete]
        public IActionResult DeletarDieta(int id) 
        {
            var dieta = _context.DIETA_USUARIO.Find(id);
            if (dieta == null)
            {
                return NotFound($"Dieta não encontrada com o id {id}");
            }
            _context.DIETA_USUARIO.Remove(dieta);
            _context.SaveChanges();
            return Ok("Produto deletado");
        }
    }
}
