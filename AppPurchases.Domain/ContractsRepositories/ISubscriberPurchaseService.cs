using AppPurchases.Domain.EventEntities;

namespace AppPurchases.Domain.ContractsRepositories
{
    public interface ISubscriberPurchaseService
    {
        void Handle(object source, PurchaseEvent purchaseEvent);
    }
}
