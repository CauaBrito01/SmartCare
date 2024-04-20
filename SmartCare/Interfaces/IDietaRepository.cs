using SmartCare.Models;

namespace SmartCare.Interfaces
{
    public interface IDietaRepository
    {
        IEnumerable<DietaUsuarioModel> List();
        DietaUsuarioModel Find(int id);
        void Add(DietaUsuarioModel dieta);
        void Put(DietaUsuarioModel dieta);
        void Delete(int id);
    }
}

