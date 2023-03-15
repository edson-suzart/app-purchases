using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using AppPurchases.Function.ContractsServices;
using AppPurchases.Function.Entities;
using System;
using System.Threading.Tasks;

namespace AppPurchases.Function.Services
{
    public class PurchaseServiceFunction : IPurchaseServiceFunction
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseServiceFunction(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task Process(PurchaseMessageModel messageModel)
        {
            var purchaseDto = ProcessPurchase(messageModel);
            await _purchaseRepository.PurchaseApp(purchaseDto);
        }

        private static PurchaseDTO ProcessPurchase(PurchaseMessageModel messageModel) =>
            new()
            {
                AppId = messageModel.AppId,
                CpfClient = messageModel.CpfClient,
                Id = messageModel.Id,
                NameApp = messageModel.NameApp,
                PurchaseDate = DateTime.Now
            };
    }
}
