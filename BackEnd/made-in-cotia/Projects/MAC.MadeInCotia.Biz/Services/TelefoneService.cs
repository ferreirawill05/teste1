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

        public IEnumerable<TelefonesInputModel> ConsultaPorId(int id)
        { 
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaboradores = @"SELECT CF_ColaboradorTelefone.Id_Telefone AS IdTelefone, CF_ColaboradorTelefone.Id_Colaborador AS IdColaborador, CF_ColaboradorTelefone.Ds_Numero AS DsNumero, CF_ColaboradorTelefone.Nm_Apelido AS NmApelido, CF_ColaboradorTelefone.Fl_Principal AS FlPrincipal
                FROM CF_ColaboradorTelefone
                WHERE CF_ColaboradorTelefone.Id_Colaborador = @id and CF_ColaboradorTelefone.Fl_Ativo = 1";

                return connection.Query<TelefonesInputModel>(colaboradores, new { id = id });
            }
        }

        public IEnumerable<TelefonesInputModel> ConsultaTodos()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var telefones = @"SELECT CF_ColaboradorTelefone.Id_Telefone AS IdTelefone, CF_ColaboradorTelefone.Id_Colaborador AS IdColaborador, CF_ColaboradorTelefone.Nm_Apelido AS NmApelido, CF_ColaboradorTelefone.Fl_Principal AS FlPrincipal
                FROM CF_ColaboradorTelefone";

                return connection.Query<TelefonesInputModel>(telefones);
            }
        }

        public TelefonesInputModel CriarTelefone (TelefonesInputModel telefone)
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

        public TelefonesInputModel DeletarTelefone(TelefonesInputModel telefone)
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

        public TelefonesInputModel AtuaizarTelefone(TelefonesInputModel telefone)
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
    }
}
