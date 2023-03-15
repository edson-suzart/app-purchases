using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppPurchases.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [Route("getApps")]
        [ProducesResponseType(typeof(IEnumerable<AppModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<AppModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRegisteredApps()
        {
            var apps = await _applicationService.GetAllAppsRegistered();

            if (apps.IsFailure) return NotFound(apps.Error);

            return Ok(apps.Value);
        }
    }
}
