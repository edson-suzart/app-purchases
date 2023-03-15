using AppPurchases.Domain.Entities;
using FluentValidation;

namespace AppPurchases.Infrastructure.Validations
{
    public class CreditCardValidation : AbstractValidator<CreditCardModel>
    {
        public CreditCardValidation()
        {
            RuleFor(c => c.NumberCard).NotEmpty().CreditCard().WithMessage("Número do cartão não é válido. ");
            RuleFor(c => c.Validate).NotEmpty().WithMessage("A validade do cartão não pode ser nula. ");
            RuleFor(c => c.CpfClient).IsValidCPF().NotEmpty().WithMessage("Cpf inserido não é válido. ");
            RuleFor(c => c.SecutiryCode).NotEmpty().WithMessage("O código de segurança não pode ser nulo. ");
        }
    }
}
