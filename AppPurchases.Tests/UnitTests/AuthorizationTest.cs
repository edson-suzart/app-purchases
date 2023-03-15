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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppPurchases.Tests.UnitTests
{
    [TestClass]
    public class AuthorizationTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IDistributedCache> _mockCache;
        private readonly ApplicationService _applicationService;
        private readonly Mock<IClientService> _mockClientService;
        private readonly Mock<IValidator<UserModel>> _validator;
        private readonly Mock<IApplicationRepository> _mockApplicationRepository;

        public AuthorizationTest()
        {
            _mockCache = new Mock<IDistributedCache>();
            _mockMapper = new Mock<IMapper>();
            _mockApplicationRepository = new Mock<IApplicationRepository>();
            _mockClientService = new Mock<IClientService>();
            _validator = new Mock<IValidator<UserModel>>();

            _validator
                .Setup(x => x.Validate(It.IsAny<UserModel>()))
                .Returns(new FluentValidation.Results.ValidationResult());
            _mockClientService
                .Setup(x => x.GetRegisteredUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Result.Success()));
            _mockMapper
                .Setup(x => x.Map<AppDTO>(It.IsAny<object>()))
                .Returns(new ApplicationStubs().GetDTO());
            _mockApplicationRepository
                .Setup(x => x.GetAllRegisteredApps())
                .Returns(Task.FromResult(new ApplicationStubs().GetListAppDtoRepository()));

            _applicationService = new ApplicationService(_mockCache!.Object, _mockMapper.Object, _mockApplicationRepository.Object);
        }

        [TestMethod]
        public async Task ShouldBeReturn200Ok_TokenCreated()
        {
            var controller = new AuthorizationController(_mockClientService.Object, _validator.Object);
            var userModel = new UserModel { Cpf = "3212312", Password = "213123" };

            var actionResult = await controller.GenerateTokenClient(userModel);
            var contentResult = actionResult as OkObjectResult;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(200, contentResult.StatusCode);
        }
    }
}