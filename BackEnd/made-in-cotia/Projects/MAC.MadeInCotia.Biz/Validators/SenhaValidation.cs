using FluentValidation;
using Mac.MadeInCotia.Entities.Colaborador;

namespace MAC.MadeInCotia.Biz.Validators
{
    public class SenhaValidation : AbstractValidator<ColaboradorAlterarSenhaViewModel>
    {
        public SenhaValidation() 
        {
            RuleFor(colaborador => colaborador.senhaNova)
            .NotEmpty().WithMessage("A senha precisa ser preenchida")
            .Must(senha => ContemCaracterEspecial(senha))
            .MinimumLength(8)
            .Must(senha => ContemLetraMaiuscula(senha))
            .Must(senha => ContemNumero(senha));

            RuleFor(colaborador => colaborador.confirmarSenha)
            .NotEmpty().WithMessage("A senha precisa ser preenchida")
            .Must(senha => ContemCaracterEspecial(senha))
            .MinimumLength(8)
            .Must(senha => ContemLetraMaiuscula(senha))
            .Must(senha => ContemNumero(senha));
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
    }
}
