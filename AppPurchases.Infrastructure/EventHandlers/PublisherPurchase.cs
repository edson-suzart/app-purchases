using AppPurchases.Domain.ContractsRepositories;
using AppPurchases.Domain.DTOs;
using AppPurchases.Domain.EventEntities;

namespace AppPurchases.Infrastructure.EventHandlers
{
    public class PublisherPurchase : IHandlePurchase
    {
        public event EventHandler<PurchaseEvent> PurchaseMade;
        private readonly ISubscriberPurchaseService _subscriber;

        public PublisherPurchase(ISubscriberPurchaseService subscriber)
        {
            _subscriber = subscriber;
        }

        protected virtual void OnPurchaseMade(PurchaseDTO purchaseDTO)
        {
            PurchaseMade?.Invoke(this, new PurchaseEvent(purchaseDTO));
        }

        public void SendPurchaseEvent(PurchaseDTO purchaseDTO)
        {
            PurchaseMade += _subscriber.Handle;
            OnPurchaseMade(purchaseDTO);
        }
    }
}
