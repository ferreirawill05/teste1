using Mac.MadeInCotia.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Mac.MadeInCotia.Data.Context
{
    public class MacMadeInCotiaContext : DbContext
    {
        public MacMadeInCotiaContext() : base()
        {
        }

        public MacMadeInCotiaContext(DbContextOptions<MacMadeInCotiaContext> options) : base(options) { }

        public DbSet<CF_ColaboradorTipoUsuario> CF_ColaboradorTipoUsuario { get; set; }
        public DbSet<CF_Colaborador> CF_Colaborador { get; set; }
        public DbSet<CF_ColaboradorEmail> CF_ColaboradorEmail { get; set; }
        public DbSet<CF_ColaboradorTelefone> CF_ColaboradorTelefone { get; set; }
        public DbSet<DF_Permissao> DF_Permissao { get; set; }
        public DbSet<DF_ColaboradorPermissao> DF_ColaboradorPermissao { get; set; }
    }
}
