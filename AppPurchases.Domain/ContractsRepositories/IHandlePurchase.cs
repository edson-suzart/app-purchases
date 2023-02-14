using AppPurchases.Domain.DTOs;

namespace AppPurchases.Domain.ContractsRepositories
{
    public interface IHandlePurchase
    {
        void SendPurchaseEvent(PurchaseDTO purchaseDTO);
    }
}
