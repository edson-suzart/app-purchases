using AppPurchases.Application.Entities;
using CSharpFunctionalExtensions;

namespace AppPurchases.Application.ContractsServices
{
    public interface IClientService
    {
        Task<Result> RegiterNewClient(RegisterClientModel model);
        Task<Result> RegisterNewCreditCardToClient(CreditCardModel creditCard);
        Task<Result> GetRegisteredUser(string username, string password);
    }
}
