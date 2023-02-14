namespace AppPurchases.Shared.Entities
{
    public interface IMessageBrokerService
    {
        void SendMessageToQueue<T>(T anyMessage, string queue);
    }
}