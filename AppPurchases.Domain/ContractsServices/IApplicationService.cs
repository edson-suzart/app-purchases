using AppPurchases.Domain.Entities;
using CSharpFunctionalExtensions;

namespace AppPurchases.Domain.ContractsServices
{
    public interface IApplicationService
    {
        Task<Result<List<AppModel>>> GetAllAppsRegistered();
    }
}
