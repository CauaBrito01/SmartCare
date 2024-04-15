using SmartCare.Models;

namespace SmartCare.Interfaces
{
    public interface IUsuarioRepository
    {
        List<UsuarioModel> ListarUsuarios();
        UsuarioModel ListaUsuario(int id);
        void GravarUsuario(UsuarioModel usuario);
        void EditarUsuario(UsuarioModel usuario);
        void DeletarUsuario(int id);
    }

}
