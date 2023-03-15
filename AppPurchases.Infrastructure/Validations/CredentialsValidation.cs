using AppPurchases.Domain.Entities;
using FluentValidation;

namespace AppPurchases.Infrastructure.Validations
{
    public class CredentialsValidation : AbstractValidator<UserModel>
    {
        public CredentialsValidation()
        {
            RuleFor(c => c.Cpf).NotEmpty().IsValidCPF().WithMessage("O cpf não está válido. ");
            RuleFor(c => c.Password).NotEmpty().WithMessage("A senha não pode ser nula. ");
        }
    }
}
