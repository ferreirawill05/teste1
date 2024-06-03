using Dapper;
using Mac.Common.Util.Core;
using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Colaborador;
using Mac.MadeInCotia.Entities.Emails;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MAC.MadeInCotia.Biz.Services
{
    public class ColaboradorService
    { 

        private readonly IConfiguration _configuration;
        private readonly MacMadeInCotiaContext _context;
        
        public ColaboradorService(IConfiguration configuration, MacMadeInCotiaContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        public IEnumerable<ColaboradorViewModel> ConsultaPorId(int id)
        {
/*          var connectionString = _configuration.GetConnectionString("DefaultConnection");*/
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                var colaborador = @"SELECT CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Usuario AS NmUsuario, CF_Colaborador.Ds_Senha AS Senha, CF_Colaborador.Fl_Ativo AS FlAtivo
                FROM CF_Colaborador
                WHERE CF_Colaborador.Id_TipoUsuario = @id and CF_Colaborador.Fl_Ativo = 1";

                return connection.Query<ColaboradorViewModel>(colaborador, new {id = id});
            }
        }

        public IEnumerable<ColaboradorViewModel> ConsultaTodos()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Usuario AS NmUsuario, CF_Colaborador.Ds_Senha AS Senha, CF_Colaborador.Fl_Ativo AS FlAtivo
                FROM CF_Colaborador";

                return connection.Query<ColaboradorViewModel>(colaborador);
            }
        }


        public ColaboradorViewModel CriarUsuario(ColaboradorViewModel colaborador) 
        {
            CF_Colaborador colaboradorBanco = new CF_Colaborador(
                colaborador.IdTipoUsuario,
                colaborador.Nome,
                colaborador.Cpf,
                colaborador.NmUsuario,
                Crypt.Encrypt(colaborador.Senha),
                true,
                DateTime.Now
            ) ;

            _context.CF_Colaborador.Add( colaboradorBanco );
            _context.SaveChanges();
            colaborador.Senha = string.Empty;
            return (colaborador);                      
        }

        public ColaboradorViewModel DeletarUsuario(ColaboradorViewModel colaborador)
        {
            CF_Colaborador? colaboradorBanco = _context.CF_Colaborador
                .FirstOrDefault(c => c.Id_Colaborador == colaborador.IdColaborador);

            if (colaboradorBanco != null)
            {
                _context.CF_Colaborador.Remove(colaboradorBanco);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Colaborador not found");
            }

            return colaborador;
        }

        public ColaboradorViewModel AtualizarUsuario(ColaboradorViewModel colaborador)
        {
            var colaboradorBanco = _context.CF_Colaborador.Find(colaborador.IdColaborador);

            if (colaboradorBanco == null)
            {
                throw new ArgumentException("Colaborador not found.");
            }

            colaboradorBanco.Id_TipoUsuario = colaborador.IdTipoUsuario;
            colaboradorBanco.Nm_Nome = colaborador.Nome;
            colaboradorBanco.Ds_Cpf = colaborador.Cpf;
            colaboradorBanco.Nm_Usuario = colaborador.NmUsuario;
            colaboradorBanco.Ds_Senha = Crypt.Encrypt(colaborador.Senha);
            colaboradorBanco.Fl_Ativo = true; // Assuming you want to set it active on update as well
            colaboradorBanco.Dt_Criacao = DateTime.Now;

            _context.CF_Colaborador.Update(colaboradorBanco);
            _context.SaveChanges();

            return colaborador;
        }

        

        public ColaboradorAlterarSenhaViewModel AtualizarSenha(ColaboradorAlterarSenhaViewModel colaborador)
        {
            var colaboradorSenha = _context.CF_Colaborador.Find(colaborador.senhaNova);
            if (colaboradorSenha!.Ds_Senha == null)
            {
                throw new ArgumentException("Password not found.");
            }

            colaboradorSenha.Ds_Senha = Crypt.Encrypt(colaborador.senhaNova);

            _context.CF_Colaborador.Update(colaboradorSenha);
            _context.SaveChanges();

            return colaborador;
        }

        public ColaboradorViewModel AtualizarSenha(ColaboradorViewModel colaborador)
        {
            var colaboradorBanco = _context.CF_Colaborador.Find(colaborador.Senha);
            if (colaboradorBanco == null) throw new ArgumentException("Error");

            colaboradorBanco.Ds_Senha = Crypt.Encrypt(colaborador.Senha);

            _context.CF_Colaborador.Update(colaboradorBanco);
            _context.SaveChanges();

            return colaborador;
        }

        // Método gerar TOKEN

        public static string GerarToken(CF_Colaborador user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MIC@willjwt");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Nm_Nome),
                    new Claim("IdColaborador", user.Id_Colaborador.ToString()),
                    new Claim("IdPermissaoColaborador", user.Id_TipoUsuario.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //----------------------------------

        public IEnumerable<EmailsViewModel> ConsultaPorEmail(string email)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaboradores = @"SELECT CF_ColaboradorEmail.Id_Colaborador AS IdColaborador, CF_ColaboradorEmail.Id_Email AS IdEmail, CF_ColaboradorEmail.Ds_Email AS DsEmail, CF_ColaboradorEmail.Fl_Principal AS FlPrincipal
                FROM CF_ColaboradorEmail
                WHERE CF_ColaboradorEmail.Ds_Email = @email and CF_ColaboradorEmail.Fl_Principal = 1";

                return connection.Query<EmailsViewModel>(colaboradores, new { email = email });
            }
        }

        /*Filtrar por CPF*/

        public IEnumerable<ColaboradorViewModel> BuscarPorCpf(string cpf)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Nm_Usuario AS NmUsuario, CF_Colaborador.Ds_Senha AS Senha, CF_Colaborador.Fl_Ativo AS FlAtivo
                FROM CF_Colaborador
                WHERE  CF_Colaborador.Ds_Cpf AS Cpf = @cpf";

                return connection.Query<ColaboradorViewModel>(colaborador, new { cpf = cpf });
            }
        }

        /*Filtrar por Nome*/

        public IEnumerable<ColaboradorViewModel> BuscarPorUsuario(string usuario)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Usuario AS NmUsuario
                    FROM CF_Colaborador
                    WHERE CF_Colaborador.Nm_Usuario = @usuario";

                return connection.Query<ColaboradorViewModel>(colaborador, new { usuario = usuario });
            }
        }


        public IEnumerable<ColaboradorViewModel> BuscarPorPeriodo(DateTime firstDate, DateTime lastDate)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Usuario AS NmUsuario
                    FROM CF_Colaborador
                    WHERE CF_Colaborador.DT_Criacao >= @firstDate AND CF_Colaborador.DT_Criacao <= @lastDate";

                return connection.Query<ColaboradorViewModel>(colaborador, new { firstDate = firstDate, lastDate = lastDate });
            }
        }

        /*Filtro mais antigos*/

        public IEnumerable<ColaboradorViewModel> BuscarAntigo()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Usuario AS NmUsuario
                    FROM CF_Colaborador
                    ORDER BY CF_Colaborador.DT_Criacao ASC";

                return connection.Query<ColaboradorViewModel>(colaborador);
            }
        }

        /*Filtro mais recentes*/

        public IEnumerable<ColaboradorViewModel> BuscarRecente()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Usuario AS NmUsuario
                    FROM CF_Colaborador
                    ORDER BY CF_Colaborador.DT_Criacao DESC";

                return connection.Query<ColaboradorViewModel>(colaborador);
            }
        }

        //Filtro geral

        /*public List<ColaboradorAlterarSenhaViewModel> SearchUsers(ColaboradorViewModel colaboradorFiltro)
        {
            IQueryable<CF_Colaborador> query = _context.Set<CF_Colaborador>();

            if (!string.IsNullOrEmpty(colaboradorFiltro.Id))
            {
                int userId = (colaboradorFiltro.IdColaborador);
                query = query.Where(user => user.IdColaborador == userId);
            }

            if (!string.IsNullOrEmpty(colaboradorFiltro.Nome))
            {
                query = query.Where(user => EF.Functions.Like(user.Nm_Usuario, $"%{colaboradorFiltro.Nome}%"));
            }

            if (!string.IsNullOrEmpty(colaboradorFiltro.Email))
            {
                query = query.Where(user => EF.Functions.Like(user.Email, $"%{colaboradorFiltro.Email}%"));
            }

            if (!string.IsNullOrEmpty(colaboradorFiltro.Telephone))
            {
                query = query.Where(user => EF.Functions.Like(user.Telefone, $"%{colaboradorFiltro.Telephone}%"));
            }

            return query.Select(user => new ColaboradorViewModel
            {
                Nome = user.Nome,
                Email = user.Email,

            }).ToList();
        }*/
    }
     
}
