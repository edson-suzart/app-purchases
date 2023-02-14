using AppPurchases.Domain.DTOs;

namespace AppPurchases.Domain.ContractsRepositories
{
    public interface IPurchaseRepository
    {
        Task PurchaseApp(PurchaseDTO purchaseDTO);
    }
}
