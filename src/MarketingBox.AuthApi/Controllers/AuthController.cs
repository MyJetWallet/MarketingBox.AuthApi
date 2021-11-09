using System;
using MarketingBox.AuthApi.Models.Auth;
using MarketingBox.AuthApi.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MarketingBox.AuthApi.Domain.Models.Errors;
using MarketingBox.AuthApi.Domain.Tokens;
using Role = MarketingBox.AuthApi.Domain.Tokens.Role;

namespace MarketingBox.AuthApi.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokensService _tokensService;
        private readonly TenantLocator _tenantLocator;

        public AuthController(
            ITokensService tokensService,
            TenantLocator tenantLocator)
        {
            _tokensService = tokensService;
            _tenantLocator = tenantLocator;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]

        public async Task<ActionResult<AuthenticateResponse>> LoginAsync(
            [FromBody] AuthenticateRequest request)
        {
            var tenantId = _tenantLocator.GetTenantIdByHost(Request.Host.Host);

            var (token, error) = await _tokensService.LoginAsync(request.Email, tenantId, request.Password);

            if (error != null)
            {
                switch (error.Type)
                {
                    case LoginErrorType.NoUser:
                        ModelState.AddModelError(nameof(request.Email), "There is no such user");
                        break;
                    case LoginErrorType.WrongPassword:
                        ModelState.AddModelError(nameof(request.Password), "Password is wrong");
                        break;
                    default:
                        break;
                }
                return BadRequest(ModelState);
            }

            return Ok(new AuthenticateResponse()
            {
                Token = token.Token,
                ExpiresAt = token.ExpiresAt,
                Role = token.Role switch {
                    Role.Affiliate => Models.Auth.Role.Affiliate,
                    Role.MasterAffiliate => Models.Auth.Role.MasterAffiliate,
                    Role.AffiliateManager => Models.Auth.Role.AffiliateManager,
                    Role.Admin => Models.Auth.Role.Admin,
                    _ => throw new ArgumentOutOfRangeException(nameof(token.Role), token.Role, null)
                }
            });
        }
    }
}