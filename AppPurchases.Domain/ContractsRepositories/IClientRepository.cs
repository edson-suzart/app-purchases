using AppPurchases.Domain.DTOs;

namespace AppPurchases.Domain.ContractsRepositories
{
    public interface IClientRepository
    {
        Task<RegisterDTO> GetRegisteredClient(string cpf);
        Task<RegisterDTO> GetRegisteredUser(string username, string password);
        Task RegisterNewClient(RegisterDTO register);
        Task RegisterNewCreditCardToClient(CreditCardDTO creditCard);
    }
}
