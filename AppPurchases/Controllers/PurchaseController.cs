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
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IValidator<PurchaseModel> _validator;

        public PurchaseController(IPurchaseService purchaseService, IValidator<PurchaseModel> validator)
        {
            _validator = validator;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [Route("purchaseApp")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PurchaseApp([FromBody] PurchaseModel purchase)
        {
            var validation = _validator.Validate(purchase);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var result = await _purchaseService.PurchaseApp(purchase);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok();
        }
    }
}
