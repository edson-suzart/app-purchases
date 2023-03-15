using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using AppPurchases.Shared.Enuns;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace AppPurchases.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public ClientService(
            IMapper mapper,
            IClientRepository clientRepository)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<Result> RegiterNewClient(RegisterClientModel model)
        {
            var registerDto = _mapper.Map<RegisterDTO>(model);
            var client = await _clientRepository.GetRegisteredClient(registerDto.CpfClient!);

            if (client is null)
            {
                await _clientRepository.RegisterNewClient(registerDto);
                return Result.Success();
            }

            return Result.Failure("O cliente já está cadastrado na base de dados. ");
        }

        public async Task<Result> GetRegisteredUser(string username, string password)
        {
            var client = await _clientRepository.GetRegisteredUser(username, password);
            if (client is null)
                return Result.Failure("Login ou senha incorretos. ");

            return Result.Success();
        }

        public async Task<Result> RegisterNewCreditCardToClient(CreditCardModel creditCard)
        {
            var creditCardDto = _mapper.Map<CreditCardDTO>(creditCard);
            var client = await _clientRepository.GetRegisteredClient(creditCardDto.CpfClient!);
            var validations = ValidateBeforeRegister(client, creditCardDto);
            if (validations.IsFailure)
                return Result.Failure(validations.Error);

            await _clientRepository.RegisterNewCreditCardToClient(creditCardDto);
            return Result.Success();
        }

        private static Result ValidateBeforeRegister(RegisterDTO? client, CreditCardDTO creditCard)
        {
            if (client is null)
                return Result.Failure("Usuário para qual quer cadastrar o cartão de crédito não existe na base de dados. ");

            if (client.CreditCard is not null)
            {
                if (client.CreditCard!.Where(c => c.NumberCard!.Equals(creditCard.NumberCard)).Any())
                    return Result.Failure("Este cartão já foi adicionado anteriormente. Por favor, adicione um cartão diferente. ");

                if (creditCard.CreditCardType.Equals(CreditCardEnum.Primary))
                    return Result.Failure("O cliente não pode ter mais de um cartão principal cadastrado.");
            }

            return Result.Success();
        }
    }
}
