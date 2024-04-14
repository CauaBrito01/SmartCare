using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.Connection;
using SmartCare.Models;

namespace SmartCare.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly ConnectionDb _context;


        public UsuarioController(ConnectionDb context)
        {
            _context = context;
        }


        [HttpPost("login")]
        public IActionResult ValidaLogin(string email, string senha)
        {
            bool emailExists = _context.USUARIO.Any(d => d.email == email);
            bool passwordMatches = _context.USUARIO.Any(d => d.email == email && d.senha == senha);

            if (!emailExists)
            {
                return NotFound("Não existe usuario com o email especificado.");
            }
            else if (!passwordMatches)
            {
                return Unauthorized("Senha incorreta.");
            }

            return Ok("Usuário autenticado com sucesso.");
        }



        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _context.USUARIO.ToList();
            if (usuarios.Count == 0)
            {
                return NotFound("Sem usuarios disponiveis");
            }

            return Ok(usuarios);
        }

        [HttpGet("usuario/{id}")]
        public IActionResult ListaUsuario(int id)
        {
            var usuario = _context.USUARIO.Find(id);
            if (usuario == null)
            {
                return NotFound("Usuario não existe");
            }
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult GravarUsuario(UsuarioModel model)
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok("Usuario Criado");
        }

        [HttpPut]
        public IActionResult EditarUsuario(UsuarioModel model)
        {
            if (model == null || model.ID_USUARIO == 0)
            {
                if (model == null)
                {
                    return BadRequest("Model Data não e valida");
                }

                else if (model.ID_USUARIO == 0)
                {
                    return BadRequest($" Id {model.ID_USUARIO} não é um id valido");
                }
            }
            var usuario = _context.USUARIO.Find(model.ID_USUARIO);
            if (usuario == null)
            {
                return NotFound($" Usuario com o Id {model.ID_USUARIO} não encontrado");
            }
            usuario.NOME_USUARIO = model.NOME_USUARIO;
            //usuario.DAT_NASCIMENTO = model.DAT_NASCIMENTO;
            usuario.CPF_USUARIO = model.CPF_USUARIO;
            usuario.ID_ELENCO = model.ID_ELENCO;
            usuario.IND_VIGENTE = model.IND_VIGENTE;
            usuario.email = model.email;
            usuario.senha = model.senha;

            _context.SaveChanges();

            return Ok("Usuario Editado");
        }

        [HttpDelete]
        public IActionResult DeletarUsuario(int id)
        {
            try
            {
                var usuario = _context.USUARIO.Find(id);
                if (usuario == null)
                {
                    return NotFound($"usuario não encontrado com o id {id}");
                }
                _context.USUARIO.Remove(usuario);
                _context.SaveChanges();
                return Ok("Usuario deletado");
            }
            catch (Exception)
            {

                return BadRequest("Usuario vinculado a uma dieta, portanto exclusão não é possivel");
            }
        }
    }
}
