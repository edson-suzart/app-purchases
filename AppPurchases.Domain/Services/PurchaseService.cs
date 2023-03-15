using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using AppPurchases.Shared.Enuns;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace AppPurchases.Domain.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IMapper _mapper;
        private readonly IHandlePurchase _handlePurchase;
        private readonly IClientRepository _clientRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IApplicationRepository _applicationRepository;

        public PurchaseService(
            IMapper mapper,
            IHandlePurchase handlePurchase,
            IPurchaseRepository orderRepository,
            IClientRepository clientRepository,
            IApplicationRepository applicationRepository)
        {
            _mapper = mapper;
            _handlePurchase = handlePurchase;
            _purchaseRepository = orderRepository;
            _clientRepository = clientRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<Result> PurchaseApp(PurchaseModel purchase)
        {
            var purchaseDto = _mapper.Map<PurchaseDTO>(purchase);
            var client = await _clientRepository.GetRegisteredClient(purchase.CpfClient!);
            var app = await _applicationRepository.GetRegisteredApp(purchase.AppId);

            var validations = ValidateBeforePurchase(client, app);
            if (validations.IsSuccess)
                _handlePurchase.SendPurchaseEvent(purchaseDto);

            return validations;
        }

        private static Result ValidateBeforePurchase(RegisterDTO? client, AppDTO? app)
        {
            if (client is null)
                return Result.Failure("Não foi possível realizar uma compra. O cliente não está cadastrado. ");

            if (client.CreditCard?.Count == default)
                Result.Failure("Não foi possível realizar a compra. Não existe nenhum cartão válido cadastrado para o cliente. ");

            if (app is null)
                return Result.Failure("Não foi possível realizar uma compra. O aplicativo não está disponível na base de dados. ");

            var primaryCreditCard = client.CreditCard!.OfType<CreditCardDTO>().Where(c => c.CreditCardType.Equals(CreditCardEnum.Primary)).First();
            if (primaryCreditCard.CreditLimit < app.PriceApp)
                return Result.Failure("Não foi possível realizar uma compra. O valor do aplicativo excede o limite disponível do cartão de crédito. ");

            return Result.Success();
        }
    }
}
