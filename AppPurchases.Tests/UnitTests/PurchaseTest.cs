using AppPurchases.Api.Controllers;
using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
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
    public class PurchaseTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly PurchaseService _applicationService;
        private readonly Mock<IPurchaseService> _mockPurchaseService;
        private readonly Mock<IPurchaseRepository> _mockPurchaseRepository;
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<IApplicationRepository> _mockAppRepository;
        private readonly Mock<IHandlePurchase> _mockHandle;
        private readonly Mock<IValidator<PurchaseModel>> _validator;

        public PurchaseTest()
        {
            _validator = new Mock<IValidator<PurchaseModel>>();
            _mockMapper = new Mock<IMapper>();
            _mockHandle = new Mock<IHandlePurchase>();
            _mockClientRepository = new Mock<IClientRepository>();
            _mockPurchaseService = new Mock<IPurchaseService>();
            _mockPurchaseRepository = new Mock<IPurchaseRepository>();
            _mockAppRepository = new Mock<IApplicationRepository>();

            _validator
                .Setup(x => x.Validate(It.IsAny<PurchaseModel>()))
                .Returns(new FluentValidation.Results.ValidationResult());
            _mockAppRepository
                .Setup(x => x.GetRegisteredApp(It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationStubs().GetDTO()));
            _mockClientRepository
                .Setup(x => x.GetRegisteredClient(It.IsAny<string>()))
                .Returns(Task.FromResult(new RegisterStub().GetRegisterDTO()));
            _mockPurchaseService
                .Setup(x => x.PurchaseApp(It.IsAny<PurchaseModel>()))
                .Returns(Task.FromResult(Result.Success()));
            _mockPurchaseRepository
                .Setup(x => x.PurchaseApp(It.IsAny<PurchaseDTO>()))
                .Returns(Task.FromResult(new ApplicationStubs().GetListAppDtoRepository()));

            _applicationService = new PurchaseService(
                _mockMapper.Object, 
                _mockHandle.Object,
                _mockPurchaseRepository.Object,
                _mockClientRepository.Object,
                _mockAppRepository.Object);
        }

        [TestMethod]
        public async Task ShouldBeReturn200Ok_PurchaseApp()
        {
            var controller = new PurchaseController(_mockPurchaseService.Object, _validator.Object);
            var purchaseModel = new PurchaseModel { AppId = "1", CpfClient = "2321312", Id = "1", NameApp = "Teste" };

            var actionResult = await controller.PurchaseApp(purchaseModel);
            var value = actionResult as OkResult;

            Assert.AreEqual(200, value!.StatusCode);
        }
    }
}