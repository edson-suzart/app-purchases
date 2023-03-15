using AppPurchases.Application.DTOs;

namespace AppPurchases.Application.ContractsRepositories
{
    public interface IPurchaseRepository
    {
        Task PurchaseApp(PurchaseDTO purchaseDTO);
    }
}
