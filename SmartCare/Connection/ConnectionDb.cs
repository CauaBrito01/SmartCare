using Microsoft.EntityFrameworkCore;
using SmartCare.Models;

namespace SmartCare.Connection
{
    public class ConnectionDb : DbContext
    {
        public ConnectionDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DietaUsuarioModel> DIETA_USUARIO { get; set;}
        public DbSet<ProfissionalModel> PROFISSIONAL { get; set; }
        public DbSet<UsuarioModel> USUARIO { get; set; }

    }
}
