using Dapper;
using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Telefones;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MAC.MadeInCotia.Biz.Services
{
    public class TelefoneService
    {
        private readonly IConfiguration _configuration;
        private readonly MacMadeInCotiaContext _context;

        public TelefoneService(IConfiguration configuration, MacMadeInCotiaContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IEnumerable<TelefonesViewModel> ConsultaPorId(int id)
        { 
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaboradores = @"SELECT CF_ColaboradorTelefone.Id_Telefone AS IdTelefone, CF_ColaboradorTelefone.Id_Colaborador AS IdColaborador, CF_ColaboradorTelefone.Ds_Numero AS DsNumero, CF_ColaboradorTelefone.Nm_Apelido AS NmApelido, CF_ColaboradorTelefone.Fl_Principal AS FlPrincipal
                FROM CF_ColaboradorTelefone
                WHERE CF_ColaboradorTelefone.Id_Colaborador = @id and CF_ColaboradorTelefone.Fl_Ativo = 1";

                return connection.Query<TelefonesViewModel>(colaboradores, new { id = id });
            }
        }

        public IEnumerable<TelefonesViewModel> ConsultaTodos()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var telefones = @"SELECT CF_ColaboradorTelefone.Id_Telefone AS IdTelefone, CF_ColaboradorTelefone.Id_Colaborador AS IdColaborador, CF_ColaboradorTelefone.Nm_Apelido AS NmApelido, CF_ColaboradorTelefone.Fl_Principal AS FlPrincipal
                FROM CF_ColaboradorTelefone";

                return connection.Query<TelefonesViewModel>(telefones);
            }
        }

        public TelefonesViewModel CriarTelefone (TelefonesViewModel telefone)
        {
            CF_ColaboradorTelefone colaboradorTelefone = new CF_ColaboradorTelefone
            (
                telefone.IdColaborador,
                telefone.NmApelido,
                telefone.DsNumero,
                true,
                true,
                DateTime.Now
            );
            _context.CF_ColaboradorTelefone.Add(colaboradorTelefone);
            _context.SaveChanges();
            return(telefone);
        }

        public TelefonesViewModel DeletarTelefone(TelefonesViewModel telefone)
        {
            CF_ColaboradorTelefone colaboradorTelefone = new CF_ColaboradorTelefone
            (
                telefone.IdColaborador,
                telefone.NmApelido,
                telefone.DsNumero,
                true,
                true,
                DateTime.Now
            );
            _context.CF_ColaboradorTelefone.Remove(colaboradorTelefone);
            _context.SaveChanges();
            return (telefone);
        }

        public TelefonesViewModel AtuaizarTelefone(TelefonesViewModel telefone)
        {
            CF_ColaboradorTelefone? colaboradorTelefone = _context.CF_ColaboradorTelefone.Find(telefone.IdTelefone);

            if (colaboradorTelefone == null)
            {
                throw new ArgumentException("Colaborador not found.");
            }

            _context.CF_ColaboradorTelefone.Update(colaboradorTelefone);
            _context.SaveChanges();
            return (telefone);
        }

        public TelefonesViewModel AtualizarFlag(TelefonesViewModel telefone)
        {
            CF_ColaboradorTelefone? flagTelefone = _context.CF_ColaboradorTelefone.Find(telefone.);
        }
    }
}
