using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Data
{
    public class EquipoContext : DbContext
    {

        public EquipoContext(DbContextOptions<EquipoContext> options) : base(options)
        {

        }

        public DbSet<Equipo> Equipos { get; set; }
    }
}
