using FluentValidation;
using Mac.MadeInCotia.Entities.Emails;

namespace MAC.MadeInCotia.Biz.Validators
{
    public class EmailValidation : AbstractValidator<EmailsViewModel>
    {
        public EmailValidation() 
        {
            RuleFor(email => email.DsEmail)
                .NotNull()
                .NotEmpty().WithMessage("O email precisar ser inserido!")
                .EmailAddress();
        }
    }
}
