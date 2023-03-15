using AppPurchases.Application.EventEntities;

namespace AppPurchases.Application.ContractsRepositories
{
    public interface ISubscriberPurchaseService
    {
        void Handle(object source, PurchaseEvent purchaseEvent);
    }
}
