using AppPurchases.Domain.ContractsServices;
using AppPurchases.Domain.Entities;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppPurchases.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class RegisterController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IValidator<RegisterClientModel> _validatorClient;
        private readonly IValidator<CreditCardModel> _validatorCreditCard;
        public RegisterController(
            IClientService clientService,
            IValidator<RegisterClientModel> validatorClient,
            IValidator<CreditCardModel> validatorCreditCard)
        {
            _validatorClient = validatorClient;
            _validatorCreditCard = validatorCreditCard;
            _clientService = clientService;
        }

        [HttpPost]
        [Route("registerNewClient")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RegisterNewClient([FromBody] RegisterClientModel register)
        {
            var validation = _validatorClient.Validate(register);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var validationCreditCard = _validatorCreditCard.Validate(register.CreditCard!.Single());
            if (!validationCreditCard.IsValid)
            {
                return BadRequest(validationCreditCard.Errors.Select(e => e.ErrorMessage));
            }

            var result = await _clientService.RegiterNewClient(register);

            if (result.IsFailure)
                return Conflict(result.Error);

            return Created("Cliente cadastrado. ", null);
        }

        [HttpPost]
        [Route("registerCreditCard")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterCreditCard([FromBody] CreditCardModel creditCard)
        {
            var validation = _validatorCreditCard.Validate(creditCard);

            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var result = await _clientService.RegisterNewCreditCardToClient(creditCard);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok();
        }
    }
}
