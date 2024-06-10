using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCare.Connection;
using SmartCare.Models;

namespace SmartCare.Controllers
{
    [Route("api/profissional")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        private readonly ConnectionDb _context;


        public ProfissionalController(ConnectionDb context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult ListaProfissional(int id)
        {
            var profissional = _context.PROFISSIONAL.Find(id);
            if (profissional == null)
            {
                return NotFound("Profissional não existe");
            }
            return Ok(profissional);
        }


        //[HttpPost("login")]
        //public IActionResult ValidaLogin(string email, string senha)
        //{
        //    bool emailExists = _context.PROFISSIONAL.Any(d => d.email == email);
        //    bool passwordMatches = _context.PROFISSIONAL.Any(d => d.email == email && d.senha == senha);

        //    if (!emailExists)
        //    {
        //        return NotFound("Não existe profissional com o email especificado.");
        //    }
        //    else if (!passwordMatches)
        //    {
        //        return Unauthorized("Senha incorreta.");
        //    }

        //    return Ok("Usuário autenticado com sucesso.");
        //}

        //INTERAÇÕES DO PROFISSIONAL COM O USUARIO

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

        //[HttpPut]
        //public IActionResult Put(UsuarioModel model)
        //{
        //    if (model == null || model.ID_USUARIO == 0)
        //    {
        //        if (model == null)
        //        {
        //            return BadRequest("Model Data não e valida");
        //        }

        //        else if (model.ID_USUARIO == 0)
        //        {
        //            return BadRequest($" Id {model.ID_USUARIO} não é um id valido");
        //        }
        //    }
        //    var usuario = _context.USUARIO.Find(model.ID_USUARIO);
        //    if (usuario == null)
        //    {
        //        return NotFound($" Usuario com o Id {model.ID_USUARIO} não encontrado");
        //    }
        //    usuario.NOME_USUARIO = model.NOME_USUARIO;
        //    //usuario.DAT_NASCIMENTO = model.DAT_NASCIMENTO;
        //    usuario.CPF_USUARIO = model.CPF_USUARIO;
        //    usuario.ID_ELENCO = model.ID_ELENCO;
        //    usuario.IND_VIGENTE = model.IND_VIGENTE;
        //    usuario.EMAIL_USUARIO = model.EMAIL_USUARIO;
        //    usuario.SENHA_USUARIO = model.SENHA_USUARIO;

        //    _context.SaveChanges();

        //    return Ok("Usuario Editado");
        //}

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioModel model)
        {
            if (model == null)
            {
                return BadRequest("Dados do modelo não são válidos");
            }

            var usuario = _context.USUARIO.Find(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com o ID {id} não encontrado");
            }

            usuario.NOME_USUARIO = model.NOME_USUARIO;
            //usuario.DAT_NASCIMENTO = model.DAT_NASCIMENTO;
            usuario.CPF_USUARIO = model.CPF_USUARIO;
            usuario.ID_ELENCO = model.ID_ELENCO;
            usuario.IND_VIGENTE = model.IND_VIGENTE;
            usuario.EMAIL_USUARIO = model.EMAIL_USUARIO;
            usuario.SENHA_USUARIO = model.SENHA_USUARIO;

            _context.SaveChanges();

            return Ok("Usuário editado");
        }

        [Authorize]
        [HttpDelete("{id}")]
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
