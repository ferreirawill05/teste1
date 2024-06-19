using Mac.Common.Util.Core;
using Mac.MadeInCotia.Entities.Colaborador;
using FluentValidation;

namespace MAC.MadeInCotia.Biz.Validators
{
    public class UsuarioValidator : AbstractValidator<ColaboradorViewModel>
    {

        public UsuarioValidator()
        {
            RuleFor(colaborador => colaborador.Senha)
            .NotEmpty().WithMessage("A senha precisar ser preenchido")
            .Must(senha => ContemCaracterEspecial(senha))
            .MinimumLength(8)
            .Must(senha => ContemLetraMaiuscula(senha))
            .Must(senha => ContemNumero(senha));

            RuleFor(colaborador => colaborador.Cpf)
            .Must(cpf => ValidaCPF(cpf));
        }
        private bool ContemCaracterEspecial(string senha)
        {
            return senha.Any(c => !char.IsLetterOrDigit(c));
        }

        private bool ContemLetraMaiuscula(string senha)
        {
            return senha.Any(char.IsUpper);
        }

        private bool ContemNumero(string senha)
        {
            return senha.Any(char.IsNumber);
        }
        private bool ValidaCPF(string cpf)
        {
            return Validacao.ValidaCPF(cpf);
        }
    }
}
