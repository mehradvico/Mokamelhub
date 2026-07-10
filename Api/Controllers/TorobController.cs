using Api.Authentication.Torob;
using Application.Services.ProductSrvs.TorobSrv.Dto;
using Application.Services.ProductSrvs.TorobSrv.Iface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/torob/v3/products")]
    [Authorize(AuthenticationSchemes = TorobAuthenticationDefaults.AuthenticationScheme
    )]
    public sealed class TorobController : ControllerBase
    {
        private readonly ITorobService _torobService;

        public TorobController(ITorobService torobService)
        {
            _torobService = torobService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TorobResponseDto), 200)]
        [ProducesResponseType(typeof(TorobErrorDto), 400)]
        [ProducesResponseType(typeof(TorobErrorDto), 401)]
        public async Task<IActionResult> GetProducts(
            [FromBody] TorobRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    new TorobErrorDto("invalid json request body")
                );
            }

            var result = await _torobService.GetProductsAsync(
                request,
                cancellationToken
            );

            if (!result.IsSuccess)
            {
                return BadRequest(
                    new TorobErrorDto(result.Error)
                );
            }

            return Ok(result.Data);
        }
    }
}