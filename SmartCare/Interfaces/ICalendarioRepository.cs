using SmartCare.Models;

namespace SmartCare.Interfaces
{
    public interface ICalendarioRepository
    {
        IEnumerable<CalendarioModel> List();
        CalendarioModel Find(int id);
        void Add(CalendarioModel dieta);
        void Put(CalendarioModel dieta);
        void Delete(int id);
    }
}
