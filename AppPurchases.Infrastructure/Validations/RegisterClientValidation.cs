using AppPurchases.Domain.Entities;
using FluentValidation;

namespace AppPurchases.Infrastructure.Validations
{
    public class RegisterClientValidation : AbstractValidator<RegisterClientModel>
    {
        public RegisterClientValidation()
        {
            RuleFor(c => c.NameClient).NotEmpty().WithMessage("O nome do cliente não pode ser nulo.");
            RuleFor(c => c.AddressClient).NotEmpty().WithMessage("O endereço do cliente não pode ser nulo.");
            RuleFor(x => x.CpfClient).NotEmpty().IsValidCPF().WithMessage("CPF inserido não é válido. ");
            RuleFor(x => x.Password).NotEmpty().WithMessage("A senha de cadastro não pode ser nula. ");
            RuleFor(x => x.UserName).Equal(x => x.CpfClient).NotEmpty().WithMessage("O username obrigatóriamente deve ser o cpf. ");
        }
    }
}
