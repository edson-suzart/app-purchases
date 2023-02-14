using AppPurchases.Function.Entities;
using System.Threading.Tasks;

namespace AppPurchases.Function.ContractsServices
{
    public interface IPurchaseServiceFunction
    {
        Task Process(PurchaseMessageModel messageModel);
    }
}