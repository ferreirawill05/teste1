using Dapper;
using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Permissoes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MAC.MadeInCotia.Biz.Services
{
    public class PermissoesService
    {
        private readonly IConfiguration _configuration; 
        private readonly MacMadeInCotiaContext _context;

        public PermissoesService(IConfiguration configuration, MacMadeInCotiaContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IEnumerable<PermissoesViewModel> ConsultaPorId(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var permissoes = @"SELECT DF_Permissao.Id_Permissao AS IdPermissao, DF_Permissao.Nm_Permissao AS NmPermissao, DF_Permissao.Fl_Ativo AS FlAtivo, DF_Permissao.Dt_Criacao AS DtCriacao
                FROM DF_Permissao
                WHERE DF_Permissao.Id_Permissao = @id and DF_Permissao.Fl_Ativo = 1";
                return connection.Query<PermissoesViewModel>(permissoes, new { id = id });
            }
        }

        public IEnumerable<PermissoesViewModel> ConsultaTodos()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var permissaoTotal = @"SELECT DF_Permissao.Id_Permissao AS IdPermissao, DF_Permissao.Nm_Permissao AS NmPermissao, DF_Permissao.Fl_Ativo AS FlAtivo, DF_Permissao.Dt_Criacao AS DtCriacao
                FROM DF_Permissao";

                return connection.Query<PermissoesViewModel>(permissaoTotal);
            }
        }

        public PermissoesViewModel CriarPermissao(PermissoesViewModel permissao)
        {
            DF_Permissao colaboradorPermissao = new DF_Permissao
            (
                permissao.Id_Permissao,
                permissao.Nm_Permissao,
                true,
                DateTime.Now
            );
            _context.DF_Permissao.Add(colaboradorPermissao);
            _context.SaveChanges();
            return (permissao);
            
        }
        
        public PermissoesViewModel DeletarPermissao(PermissoesViewModel permissao)
        {
            DF_Permissao colaboradorPermissao = new DF_Permissao
            (
                permissao.Id_Permissao,
                permissao.Nm_Permissao,
                true,
                DateTime.Now
            );
            _context.DF_Permissao.Remove(colaboradorPermissao);
            _context.SaveChanges();
            return (permissao);

        }

        public PermissoesViewModel AtualizarPermissao(PermissoesViewModel permissao)
        {
            DF_Permissao? colaboradorPermissao = _context.DF_Permissao.Find(permissao.Id_Permissao);

            if (permissao == null)
            {
                throw new ArgumentException("Colaborador not found.");
            }
            _context.DF_Permissao.Update(colaboradorPermissao);
            _context.SaveChanges ();
            return (permissao);
        }

    }
}
