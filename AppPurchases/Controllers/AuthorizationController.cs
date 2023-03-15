using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using AppPurchases.Shared.Entities;
using AppPurchases.Shared.Services;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppPurchases.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IValidator<UserModel> _validatorCredentials;
        public AuthorizationController(
            IClientService clientService,
            IValidator<UserModel> validatorCredentials)
        {
            _validatorCredentials = validatorCredentials;
            _clientService = clientService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateTokenClient([FromBody] UserModel model)
        {
            var validation = _validatorCredentials.Validate(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var user = await _clientService.GetRegisteredUser(model.Cpf!, model.Password!);
            if (user.IsFailure)
                return Unauthorized(user.Error);

            var token = TokenService.GenerateToken(new User { CpfClient = model.Cpf, Password = model.Password });
            if (token is null)
                return Unauthorized();

            return Ok(new { user = model.Cpf, token });
        }
    }
}
