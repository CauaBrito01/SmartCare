using SmartCare.Models;

namespace SmartCare.Interfaces
{
    public interface IUsuarioRepository
    {
        List<UsuarioModel> List();
        UsuarioModel Find(int id);
        void Add(UsuarioModel usuario);
        void Put(UsuarioModel usuario);
        void Delete(int id);
    }

}
