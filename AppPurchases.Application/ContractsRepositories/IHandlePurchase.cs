using AppPurchases.Application.DTOs;

namespace AppPurchases.Application.ContractsRepositories
{
    public interface IHandlePurchase
    {
        void SendPurchaseEvent(PurchaseDTO purchaseDTO);
    }
}
