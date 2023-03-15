using AppPurchases.Domain.Entities;
using FluentValidation;

namespace AppPurchases.Infrastructure.Validations
{
    public class PurchaseValidation : AbstractValidator<PurchaseModel>
    {
        public PurchaseValidation()
        {
            RuleFor(c => c.CpfClient).IsValidCPF().NotEmpty().WithMessage("O cpf do cliente não é válido. ");
            RuleFor(c => c.AppId).NotEmpty().WithMessage("O aplicativo não pode ser nulo. ");
        }
    }
}
