using AppPurchases.Domain.Entities;
using CSharpFunctionalExtensions;

namespace AppPurchases.Domain.ContractsServices
{
    public interface IClientService
    {
        Task<Result> RegiterNewClient(RegisterClientModel model);
        Task<Result> RegisterNewCreditCardToClient(CreditCardModel creditCard);
        Task<Result> GetRegisteredUser(string username, string password);
    }
}
