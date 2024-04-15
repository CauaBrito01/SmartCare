using Microsoft.EntityFrameworkCore;
using SmartCare.Connection;
using SmartCare.Interfaces;
using SmartCare.Models;

namespace SmartCare.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionDb _context;

        public UsuarioRepository(ConnectionDb context)
        {
            _context = context;
        }

        public List<UsuarioModel> ListarUsuarios()
        {
            return _context.USUARIO.ToList();
        }

        public UsuarioModel ListaUsuario(int id)
        {
            return _context.USUARIO.Find(id);
        }

        public void GravarUsuario(UsuarioModel usuario)
        {
            _context.USUARIO.Add(usuario);
            _context.SaveChanges();
        }

        public void EditarUsuario(UsuarioModel usuario)
        {
            // Busca a entidade no cache local
            var existingUsuario = _context.USUARIO.Local.FirstOrDefault(d => d.ID_USUARIO == usuario.ID_USUARIO);

            if (existingUsuario != null)
            {
                // Se já está rastreando, desconecta a entidade existente
                _context.Entry(existingUsuario).State = EntityState.Detached;
            }

            // Anexa a nova entidade e marca como modificada
            _context.USUARIO.Attach(usuario);
            _context.Entry(usuario).State = EntityState.Modified;

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void DeletarUsuario(int id)
        {
            var usuario = _context.USUARIO.Find(id);
            if (usuario != null)
            {
                _context.USUARIO.Remove(usuario);
                _context.SaveChanges();
            }
        }
    }

}

