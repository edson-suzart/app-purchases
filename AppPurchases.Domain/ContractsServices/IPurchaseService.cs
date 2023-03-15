using AppPurchases.Domain.Entities;
using CSharpFunctionalExtensions;

namespace AppPurchases.Domain.ContractsServices
{
    public interface IPurchaseService
    {
        Task<Result> PurchaseApp(PurchaseModel purchase);
    }
}
