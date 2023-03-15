using AppPurchases.Api.Controllers;
using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using AppPurchases.Domain.Services;
using AppPurchases.Tests.Stubs;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppPurchases.Tests.UnitTests
{
    [TestClass]
    public class RegisterTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClientService _clientService;
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<IClientService> _mockClientService;
        private readonly Mock<IValidator<RegisterClientModel>> _validator;
        private readonly Mock<IValidator<CreditCardModel>> _validatorCreditCard;

        public RegisterTest()
        {
            _validator = new Mock<IValidator<RegisterClientModel>>();
            _validatorCreditCard = new Mock<IValidator<CreditCardModel>>();
            _mockMapper = new Mock<IMapper>();
            _mockClientRepository = new Mock<IClientRepository>();
            _mockClientService = new Mock<IClientService>();

            _mockClientService
                .Setup(x => x.GetRegisteredUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Result.Success()));
            _validatorCreditCard
                .Setup(x => x.Validate(It.IsAny<CreditCardModel>()))
                .Returns(new FluentValidation.Results.ValidationResult());
            _validator
                .Setup(x => x.Validate(It.IsAny<RegisterClientModel>()))
                .Returns(new FluentValidation.Results.ValidationResult());
            _mockClientRepository
                .Setup(x => x.GetRegisteredClient(It.IsAny<string>()))
                .Returns(Task.FromResult(new RegisterStub().GetRegisterDTO()));

            _clientService = new ClientService(
                _mockMapper.Object,
                _mockClientRepository.Object);
        }

        [TestMethod]
        public async Task ShouldBeReturn201Created_RegisterClient()
        {
            var controller = new RegisterController(_mockClientService.Object, _validator.Object, _validatorCreditCard.Object);
            var clientModel = new RegisterClientModel
            {
                Id = "1",
                AddressClient = "Rua teste",
                CpfClient = "23123123",
                CreditCard = new List<CreditCardModel>
                {
                    new CreditCardModel
                    {
                        CpfClient = "23123123",
                        CreditCardType = Shared.Enuns.CreditCardEnum.Primary,
                        Flag = "Teste",
                        Id = "1",
                        NumberCard = "23423423",
                        SecutiryCode = 10,
                        Validate = "12/25"
                    }
                },
                DateBirthClient = "31/01/1995",
                Gender = Shared.Enuns.GenderEnum.Other,
                NameClient = "Teste",
                Password = "1234"
            };

            var actionResult = await controller.RegisterNewClient(clientModel);
            var value = actionResult as CreatedResult;

            Assert.AreEqual(201, value!.StatusCode);
        }

        [TestMethod]
        public async Task ShouldBeReturn200Ok_RegisterCreditCard()
        {
            var controller = new RegisterController(_mockClientService.Object, _validator.Object, _validatorCreditCard.Object);
            var clientModel = new CreditCardModel
            {
                CpfClient = "23123123",
                CreditCardType = Shared.Enuns.CreditCardEnum.Primary,
                Flag = "Teste",
                Id = "1",
                NumberCard = "23423423",
                SecutiryCode = 10,
                Validate = "12/25"
            };

            var actionResult = await controller.RegisterCreditCard(clientModel);
            var value = actionResult as OkResult;

            Assert.AreEqual(200, value!.StatusCode);
        }
    }
}