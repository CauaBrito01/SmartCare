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

        public IEnumerable<DietaUsuarioModel> List()
        {
            return _context.DIETA_USUARIO.ToList();
        }

        public DietaUsuarioModel Find(int id)
        {
            return _context.DIETA_USUARIO.Find(id);
        }

        public void Add(DietaUsuarioModel dieta)
        {
            _context.Add(dieta);
            _context.SaveChanges();
        }

        //public void EditarDieta(DietaUsuarioModel dieta)
        //{
        //    _context.Update(dieta);
        //    _context.SaveChanges();
        //}

        public void Put(DietaUsuarioModel dieta)
        {
            var existingDieta = _context.DIETA_USUARIO.Local.FirstOrDefault(d => d.ID_DIETA == dieta.ID_DIETA);

            if (existingDieta != null)
            {
                _context.Entry(existingDieta).State = EntityState.Detached;
            }

            _context.DIETA_USUARIO.Attach(dieta);
            _context.Entry(dieta).State = EntityState.Modified;

            _context.SaveChanges();
        }




        public void Delete(int id)
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