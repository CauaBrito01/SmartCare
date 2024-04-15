using SmartCare.Models;

namespace SmartCare.Interfaces
{
    public interface IDietaRepository
    {
        IEnumerable<DietaUsuarioModel> ListarDietas();
        DietaUsuarioModel ListaDieta(int id);
        void GravarDieta(DietaUsuarioModel dieta);
        void EditarDieta(DietaUsuarioModel dieta);
        void DeletarDieta(int id);
    }
}

