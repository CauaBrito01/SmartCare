using Microsoft.EntityFrameworkCore;
using SmartCare.Connection;
using SmartCare.Interfaces;
using SmartCare.Models;

namespace SmartCare.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly ConnectionDb _context;

        public ProfissionalRepository(ConnectionDb context)
        {
            _context = context;
        }

        public ProfissionalModel ListaProfissional(int id)
        {
            return _context.PROFISSIONAL.Find(id);
        }

        public bool ValidaLogin(string email, string senha)
        {
            return _context.PROFISSIONAL.Any(d => d.email == email && d.senha == senha);
        }
    }

}
