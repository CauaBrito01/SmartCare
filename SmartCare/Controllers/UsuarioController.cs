using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCare.Connection;
using SmartCare.Models;
using SmartCare.Services;

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
            // Verifica se existe um usuário com o email especificado
            var usuario = _context.USUARIO.FirstOrDefault(d => d.EMAIL_USUARIO == email);

            if (usuario == null)
            {
                return NotFound("Não existe usuario com o email especificado.");
            }

            // Verifica se a senha corresponde
            if (usuario.SENHA_USUARIO != senha)
            {
                return Unauthorized("Senha incorreta.");
            }

            // Gera o token utilizando o usuário recuperado do banco de dados
            var token = TokenService.GenerateToken(usuario);
            return Ok(token);
        }


        [Authorize]
        [HttpGet]
        public IActionResult List()
        {
            var usuarios = _context.USUARIO.ToList();
            if (usuarios.Count == 0)
            {
                return NotFound("Sem usuarios disponiveis");
            }

            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("usuario/{id}")]
        public IActionResult Find(int id)
        {
            var usuario = _context.USUARIO.Find(id);
            if (usuario == null)
            {
                return NotFound("Usuario não existe");
            }
            return Ok(usuario);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(UsuarioModel model)
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok("Usuario Criado");
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(UsuarioModel model)
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
            usuario.EMAIL_USUARIO = model.EMAIL_USUARIO;
            usuario.SENHA_USUARIO = model.SENHA_USUARIO;

            _context.SaveChanges();

            return Ok("Usuario Editado");
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
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
