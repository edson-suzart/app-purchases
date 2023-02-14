using AppPurchases.Domain.DTOs;

namespace AppPurchases.Domain.ContractsRepositories
{
    public interface IApplicationRepository
    {
        Task<List<AppDTO>> GetAllRegisteredApps();
        Task<AppDTO> GetRegisteredApp(string? appId);
    }
}
