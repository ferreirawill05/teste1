using FluentValidation;
using Mac.MadeInCotia.Entities.Telefones;

namespace MAC.MadeInCotia.Biz.Validators
{
    public class TelefoneValidation : AbstractValidator<TelefonesViewModel>
    {
        public TelefoneValidation() 
        {
            RuleFor(t => t.DsNumero)
                .NotNull()
                .NotEmpty().WithMessage("O número de celular precisa ser inserido!")
                .Length(11, 11).WithMessage("O número de telefone precisa ter 11 caracteres!");
        }
    }
}
