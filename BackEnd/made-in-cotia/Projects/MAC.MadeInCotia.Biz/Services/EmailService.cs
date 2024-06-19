using Dapper;
using FluentValidation;
using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Emails;
using MAC.MadeInCotia.Biz.Validators;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace MAC.MadeInCotia.Biz.Services
{
    public class EmailService
    {
        private readonly EmailValidation _validation = new();
        private readonly IConfiguration _configuration;
        private readonly MacMadeInCotiaContext _context;

        public EmailService (IConfiguration configuration, MacMadeInCotiaContext context) 
        {
            _configuration = configuration;
            _context = context;
        }

        public IEnumerable<EmailsViewModel> ConsultaPorId(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var mensagem = @"SELECT CF_ColaboradorEmail.Id_Colaborador AS IdColaborador, CF_ColaboradorEmail.Id_Email AS IdEmail, CF_ColaboradorEmail.Ds_Email AS DsEmail, CF_ColaboradorEmail.Fl_Principal AS FlPrincipal
                FROM CF_ColaboradorEmail
                WHERE CF_ColaboradorEmail.Id_Colaborador = @id and CF_ColaboradorEmail.Fl_Ativo = 1";

                return connection.Query<EmailsViewModel>(mensagem, new { id = id });
            }
        }

        public IEnumerable<EmailsViewModel> ConsultaTodos()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var emailTotal = @"SELECT CF_ColaboradorEmail.Id_Colaborador AS IdColaborador, CF_ColaboradorEmail.Id_Email AS IdEmail, CF_ColaboradorEmail.Ds_Email AS DsEmail, CF_ColaboradorEmail.Fl_Principal AS FlPrincipal
                FROM CF_ColaboradorEmail";

                return connection.Query<EmailsViewModel>(emailTotal);
            }
        }

        public EmailsViewModel CriarEmail(EmailsViewModel email)
        {
            EmailValidation _validation = new EmailValidation();
            FluentValidation.Results.ValidationResult resultado = _validation.Validate(email);

            if (!resultado.IsValid)
            {
                CF_ColaboradorEmail emailBanco = new CF_ColaboradorEmail
                (
                    email.DsEmail,
                    email.IdColaborador,
                    true,
                    true,
                    DateTime.Now
                );

                _validation.ValidateAndThrow(email);
                _context.CF_ColaboradorEmail.Add(emailBanco);
                _context.SaveChanges();
                email.DsEmail = string.Empty;
            }

            return email;
        }


        public EmailsViewModel DeletarEmail(EmailsViewModel email)
        {
            CF_ColaboradorEmail? emailBanco = _context.CF_ColaboradorEmail
                .FirstOrDefault(c => c.Id_Email == email.IdEmail);

            if (emailBanco != null)
            {
                _context.CF_ColaboradorEmail.Remove(emailBanco);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Colaborador not found");
            }

            return email;
        }

        public EmailsViewModel AtualizarEmail(EmailsViewModel email)
        {
           var emailBanco = _context.CF_ColaboradorEmail.Find(email.IdColaborador);
            if (emailBanco != null)
            {
                throw new ArgumentException("Colaborador not found.");
            }

            emailBanco.Id_Email = email.IdEmail;
            emailBanco.Id_colaborador = email.IdColaborador;
            emailBanco.Ds_Email = email.DsEmail;
            emailBanco.Fl_Principal = email.FlPrincipal;
            emailBanco.Fl_Ativo = email.FlPrincipal;

            _context.CF_ColaboradorEmail.Update(emailBanco);
            _context.SaveChanges();
            return email;
        }      

    }
}
