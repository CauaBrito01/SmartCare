using Microsoft.EntityFrameworkCore;
using SmartCare.Connection;
using SmartCare.Interfaces;
using SmartCare.Models;

namespace SmartCare.Repositories
{
    public class DietaRepository : IDietaRepository
    {
        private readonly ConnectionDb _context;


        public DietaRepository(ConnectionDb context)
        {
            _context = context;
        }

        public IEnumerable<DietaUsuarioModel> ListarDietas()
        {
            return _context.DIETA_USUARIO.ToList();
        }

        public DietaUsuarioModel ListaDieta(int id)
        {
            return _context.DIETA_USUARIO.Find(id);
        }

        public void GravarDieta(DietaUsuarioModel dieta)
        {
            _context.Add(dieta);
            _context.SaveChanges();
        }

        //public void EditarDieta(DietaUsuarioModel dieta)
        //{
        //    _context.Update(dieta);
        //    _context.SaveChanges();
        //}

        public void EditarDieta(DietaUsuarioModel dieta)
        {
            // Busca a entidade no cache local
            var existingDieta = _context.DIETA_USUARIO.Local.FirstOrDefault(d => d.ID_DIETA == dieta.ID_DIETA);

            if (existingDieta != null)
            {
                // Se já está rastreando, desconecta a entidade existente
                _context.Entry(existingDieta).State = EntityState.Detached;
            }

            // Anexa a nova entidade e marca como modificada
            _context.DIETA_USUARIO.Attach(dieta);
            _context.Entry(dieta).State = EntityState.Modified;

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }



        public void DeletarDieta(int id)
        {
            var dieta = _context.DIETA_USUARIO.Find(id);
            if (dieta != null)
            {
                _context.DIETA_USUARIO.Remove(dieta);
                _context.SaveChanges();
            }
        }

    }
}