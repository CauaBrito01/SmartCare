using SmartCare.Models;
using SmartCare.Repositories;
namespace SmartCare.Interfaces
{


    public interface IProfissionalRepository
    {
        ProfissionalModel ListaProfissional(int id);
        bool ValidaLogin(string email, string senha);
    }

}
