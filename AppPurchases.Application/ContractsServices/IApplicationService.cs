using AppPurchases.Application.Entities;
using CSharpFunctionalExtensions;

namespace AppPurchases.Application.ContractsServices
{
    public interface IApplicationService
    {
        Task<Result<List<AppModel>>> GetAllAppsRegistered();
    }
}
