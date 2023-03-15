using AppPurchases.Application.DTOs;

namespace AppPurchases.Application.ContractsRepositories
{
    public interface IClientRepository
    {
        Task<RegisterDTO> GetRegisteredClient(string cpf);
        Task<RegisterDTO> GetRegisteredUser(string username, string password);
        Task RegisterNewClient(RegisterDTO register);
        Task RegisterNewCreditCardToClient(CreditCardDTO creditCard);
    }
}
