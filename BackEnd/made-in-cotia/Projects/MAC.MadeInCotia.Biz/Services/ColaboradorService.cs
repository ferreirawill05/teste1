using Dapper;
using FluentValidation;
using FluentValidation.Results;
using Mac.Common.Util.Core;
using Mac.Common.Util.Core.Model;
using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Colaborador;
using Mac.MadeInCotia.Entities.Emails;
using MAC.MadeInCotia.Biz.Converter;
using MAC.MadeInCotia.Biz.Validators;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MAC.MadeInCotia.Biz.Services
{
    public class ColaboradorService
    {
        private readonly UsuarioValidator _validation = new();
        private readonly IConfiguration _configuration;
        private readonly MacMadeInCotiaContext _context;

        public ColaboradorService(IConfiguration configuration, MacMadeInCotiaContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        public IEnumerable<ColaboradorViewModel> ConsultaPorId(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Usuario AS NmUsuario, CF_Colaborador.Ds_Senha AS Senha, CF_Colaborador.Fl_Ativo AS FlAtivo
                FROM CF_Colaborador
                WHERE CF_Colaborador.Id_Colaborador = @id and CF_Colaborador.Fl_Ativo = 1";

                return connection.Query<ColaboradorViewModel>(colaborador, new { id = id });
            }
        }


        public IEnumerable<ColaboradorListagemViewModel> ConsultaTodos()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var colaborador = @"SELECT CF_Colaborador.Id_TipoUsuario AS IdTipoUsuario, CF_Colaborador.Nm_Nome AS Nome, CF_Colaborador.Ds_Cpf AS Cpf, CF_Colaborador.Nm_Usuario AS NmUsuario, CF_Colaborador.Ds_Senha AS Senha, CF_Colaborador.Fl_Ativo AS FlAtivo
                FROM CF_Colaborador";

                return connection.Query<ColaboradorListagemViewModel>(colaborador);
            }
        }

        public int CriarUsuario(ColaboradorViewModel colaborador)
        {
            int idColaborador = 0;
            UsuarioValidator validator = new UsuarioValidator();
            ValidationResult resultado = _validation.Validate(colaborador);

            if (resultado.IsValid)
            {
                
                CF_Colaborador colaboradorBanco = new CF_Colaborador(
                    colaborador.IdTipoUsuario,
                    colaborador.Nome,
                    colaborador.Cpf,
                    colaborador.Usuario,
                    Crypt.Encrypt(colaborador.Senha),
                    true,
                    DateTime.Now
                );


                _validation.ValidateAndThrow(colaborador);
                _context.CF_Colaborador.Add(colaboradorBanco);
                _context.SaveChanges();
                idColaborador = colaboradorBanco.Id_Colaborador;
                colaborador.Senha = string.Empty;
            }

            return idColaborador;
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
            colaboradorBanco.Nm_Usuario = colaborador.Usuario;
            colaboradorBanco.Ds_Senha = Crypt.Encrypt(colaborador.Senha);
            colaboradorBanco.Fl_Ativo = true;
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

        // Método gerar TOKEN


        //----------------------------------

        /*Filtrar por Email*/

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

        /*Filtrar por período*/

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

        public ColaboradorResponse SearchUsers(ColaboradorFiltroGeral colaboradorFiltro )
        {
              IQueryable<CF_Colaborador> query = _context.Set<CF_Colaborador>()
                                                            .Include(user => user.CF_ColaboradorEmail)
                                                            .Include(user => user.CF_ColaboradorTelefone);

            int qtdColaboradores = 0;

            if (!string.IsNullOrEmpty(colaboradorFiltro.Busca))
            {
                qtdColaboradores = query.Count();
                query = query.Where(user => user.Nm_Usuario.Contains(colaboradorFiltro.Busca) ||
                                            user.Ds_Cpf.Contains(colaboradorFiltro.Busca) ||
                                            user.CF_ColaboradorEmail.Any(e => e.Ds_Email.Contains(colaboradorFiltro.Busca)) ||
                                            user.CF_ColaboradorTelefone.Any(t => t.Ds_Numero.Contains(colaboradorFiltro.Busca)));
            }
            if (colaboradorFiltro.RegistroTemporal)
            {
                query = query.OrderByDescending(t => t.Dt_Criacao);
            }
            else
            {
                query = query.OrderBy(x => x.Dt_Criacao);
            }

            var listaColaboradores = ConverterColaboradorListagem.Converter(query.ToList());
            return new ColaboradorResponse(listaColaboradores, qtdColaboradores);
        }


    }
     
}
