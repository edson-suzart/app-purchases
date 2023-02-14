using AppPurchases.Application.Entities;
using CSharpFunctionalExtensions;

namespace AppPurchases.Application.ContractsServices
{
    public interface IPurchaseService
    {
        Task<Result> PurchaseApp(PurchaseModel purchase);
    }
}
