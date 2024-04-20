using Microsoft.EntityFrameworkCore;
using SmartCare.Connection;
using SmartCare.Interfaces;
using SmartCare.Models;

namespace SmartCare.Repositories
{
    public class CalendarioRepository : ICalendarioRepository

    {
        private readonly ConnectionDb _context;


        public CalendarioRepository(ConnectionDb context)
        {
            _context = context;
        }

        public IEnumerable<CalendarioModel> List()
        {
            return _context.CALENDARIO.ToList();
        }

        public CalendarioModel Find(int id)
        {
            return _context.CALENDARIO.Find(id);
        }

        public void Add(CalendarioModel calendario)
        {
            _context.Add(calendario);
            _context.SaveChanges();
        }

        public void Put(CalendarioModel calendario)
        {
            var existingCalendario = _context.CALENDARIO.Local.FirstOrDefault(d => d.ID_CALENDARIO == calendario.ID_CALENDARIO);

            if (existingCalendario != null)
            {
                _context.Entry(existingCalendario).State = EntityState.Detached;
            }

            _context.CALENDARIO.Attach(calendario);
            _context.Entry(calendario).State = EntityState.Modified;

            _context.SaveChanges();
        }



        public void Delete(int id)
        {
            var calendario = _context.CALENDARIO.Find(id);
            if (calendario != null)
            {
                _context.CALENDARIO.Remove(calendario);
                _context.SaveChanges();
            }
        }
    }
}
