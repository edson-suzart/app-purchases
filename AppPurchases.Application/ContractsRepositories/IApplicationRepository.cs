using AppPurchases.Application.DTOs;

namespace AppPurchases.Application.ContractsRepositories
{
    public interface IApplicationRepository
    {
        Task<List<AppDTO>> GetAllRegisteredApps();
        Task<AppDTO> GetRegisteredApp(string? appId);
    }
}
