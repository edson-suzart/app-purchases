using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.EventEntities;
using AppPurchases.Shared.Entities;

namespace AppPurchases.Infrastructure.EventHandlers
{
    public class SubscriberPurchase : ISubscriberPurchaseService
    {
        private const string QUEUE_NAME = "purchases";
        private readonly IMessageBrokerService _brokerService;

        public SubscriberPurchase(IMessageBrokerService brokerService)
        {
            _brokerService = brokerService;
        }   

        public void Handle(object source, PurchaseEvent purchaseEvent)
        {
            Task.Run(() => SendMessageBroker(purchaseEvent));
        }

        private void SendMessageBroker(PurchaseEvent purchaseEvent)
        {
            _brokerService.SendMessageToQueue(purchaseEvent, QUEUE_NAME);
        }
    }
}
