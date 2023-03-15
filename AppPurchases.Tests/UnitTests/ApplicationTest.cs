using AppPurchases.Api.Controllers;
using AppPurchases.Application.ContractsRepositories;
using AppPurchases.Application.DTOs;
using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Services;
using AppPurchases.Tests.Stubs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppPurchases.Tests.UnitTests
{
    [TestClass]
    public class ApplicationTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IDistributedCache> _mockCache;
        private readonly ApplicationService _applicationService;
        private readonly Mock<IApplicationService> _mockApplicationService;
        private readonly Mock<IApplicationRepository> _mockApplicationRepository;

        public ApplicationTest()
        {
            _mockMapper = new Mock<IMapper>();
            _mockCache = new Mock<IDistributedCache>();
            _mockApplicationService = new Mock<IApplicationService>();
            _mockApplicationRepository = new Mock<IApplicationRepository>();

            _mockMapper
                .Setup(x => x.Map<AppDTO>(It.IsAny<object>()))
                .Returns(new ApplicationStubs().GetDTO());
            _mockApplicationService
                .Setup(x => x.GetAllAppsRegistered())
                .Returns(Task.FromResult(new ApplicationStubs().GetListAppModelService()));
            _mockApplicationRepository
                .Setup(x => x.GetAllRegisteredApps())
                .Returns(Task.FromResult(new ApplicationStubs().GetListAppDtoRepository()));

            _applicationService = new ApplicationService(_mockCache.Object, _mockMapper.Object, _mockApplicationRepository.Object);
        }

        [TestMethod]
        public async Task ShouldBeReturn200Ok_GetAllApps()
        {
            var controller = new ApplicationController(_mockApplicationService.Object);

            var actionResult = await controller.GetRegisteredApps();
            var contentResult = actionResult as OkObjectResult;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(200, contentResult.StatusCode);
        }
    }
}